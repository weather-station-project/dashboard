@Library('shared-library') _
import com.davidleonm.WeatherStationDashboardVariables
import com.davidleonm.GlobalVariables

pipeline {
    agent { label 'net-core-slave' }

    environment {
        SONAR_CREDENTIALS = credentials('sonarqube-token')
        
        TOOLS_FOLDER = "${WORKSPACE}/tools"
        CODECOV_PATH = "${TOOLS_FOLDER}/codecov"
        
        REACT_ROOT_FOLDER = "${WORKSPACE}/Code/src/WeatherStationProject.Dashboard.App/ClientApp"
        COVERAGE_FOLDER_PATH="${REACT_ROOT_FOLDER}/coverage/"
        COVERAGE_TEMP_FOLDER_PATH="${COVERAGE_FOLDER_PATH}/temp/"
        DOTNET_COVERAGE_REPORT_PATH="${COVERAGE_FOLDER_PATH}coverage.opencover.xml"
        REACT_COVERAGE_REPORT_PATH="${COVERAGE_FOLDER_PATH}lcov.info"
    }

    stages {
        stage('Prepare ENV') {
            steps {
                script {
                    setBuildStatus('pending', "${WeatherStationDashboardVariables.RepositoryName}")
                    
                    prepareDashboardEnvironment()
                }
            }
        }
    
        stage('SonarQube analysis') {
            environment {
                def scannerHome = tool 'Sonarqube'
            }

            steps {
                script {
                    withSonarQubeEnv('Sonarqube') {
                        sh """
                           dotnet ${scannerHome}/SonarScanner.MSBuild.dll begin \
                                  /k:Dashboard \
                                  /n:"Weather Station Dashboard" \
                                  /v:"not provided" \
                                  /d:sonar.projectDescription="Solution with the dashboard" \
                                  /d:sonar.links.homepage="https://github.com/weather-station-project/dashboard" \
                                  /d:sonar.links.scm="https://github.com/weather-station-project/dashboard.git" \
                                  /d:sonar.inclusions="Code/src/**/*.*" \
                                  /d:sonar.exclusions="**/*.spec.tsx,**/*.js,Code/tests/**/*.*,**/Startup.cs,**/Program.cs" \
                                  /d:sonar.test.inclusions="**/*.spec.tsx,Code/tests/**/*.cs" \
                                  /d:sonar.javascript.lcov.reportPaths="${REACT_COVERAGE_REPORT_PATH}" \
                                  /d:sonar.testExecutionReportPaths="${COVERAGE_FOLDER_PATH}test-report.xml" \
                                  /d:sonar.cs.opencover.reportsPaths="${DOTNET_COVERAGE_REPORT_PATH}" \
                                  /d:sonar.login=${SONAR_CREDENTIALS}
                           """
                           // https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/Examples/MSBuild/MergeWith/HowTo.md
                           
                        sh """
                           ( cd ${REACT_ROOT_FOLDER} && npm run test-coverage )
                           dotnet build ${WORKSPACE}/Code/WeatherStationProjectDashboard.sln
                           ( cd ${WORKSPACE}/Code && dotnet test WeatherStationProjectDashboard.sln \
                                                         --no-build \
                                                         /p:CollectCoverage=true \
                                                         /p:CoverletOutput=${COVERAGE_FOLDER_PATH} \
                                                         /p:MergeWith=${COVERAGE_TEMP_FOLDER_PATH}coverlet.json \
                                                         "/p:CoverletOutputFormat=\"opencover,json\"" \
                                                         -m:1 )
                           """
                        sh "dotnet ${scannerHome}/SonarScanner.MSBuild.dll end /d:sonar.login=${SONAR_CREDENTIALS}"
                    }

                    timeout(time: 10, unit: 'MINUTES') {
                        sleep(10)

                        def qg = waitForQualityGate()
                        if (qg.status != 'OK') {
                            input "Quality gate failed with status: ${qg.status}. Continue?"
                        }
                    }
                }
            }
        }
        
        stage('Upload report to Codecov') {
            steps {
                withCredentials([string(credentialsId: 'codecov-dashboard-token',
                                        variable: 'CODECOV_TOKEN')]) {
                    // https://github.com/csMACnz/Coveralls.net-Samples/blob/xunit-opencover-appveyor/appveyor.yml
                    sh """
                       ${CODECOV_PATH} -t ${CODECOV_TOKEN} -f ${DOTNET_COVERAGE_REPORT_PATH} -cF .NET
                       ${CODECOV_PATH} -t ${CODECOV_TOKEN} -f ${REACT_COVERAGE_REPORT_PATH} -cF React
                       """
                }
            }
        }

        stage('Deploy on staging') {
            steps {
                script {
                    sh 'skip'
                    /* deployDashboardServices("${GlobalVariables.StagingDockerRegistry}",
                                            "${GlobalVariables.StagingCredentialsDockerRegistryKey}",
                                            'Staging') */
                                            
                }
            }
        }
    }

    post {
        always {
            script {
                cleanImages(false, true)
            }
        }

        success {
            script {
                setBuildStatus('success', "${WeatherStationDashboardVariables.RepositoryName}")
            }
        }

        failure {
            script {
                setBuildStatus('failure', "${WeatherStationDashboardVariables.RepositoryName}")
            }
        }
    }
}
