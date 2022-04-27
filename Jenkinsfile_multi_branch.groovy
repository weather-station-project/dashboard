@Library('shared-library') _
import com.davidleonm.WeatherStationDashboardVariables
import com.davidleonm.GlobalVariables

pipeline {
    agent { label 'net-core-slave' }

    environment {
        SONAR_CREDENTIALS = credentials('sonarqube-token')
        
        TOOLS_FOLDER = "${WORKSPACE}/tools/"
        CODECOV_PATH = "${TOOLS_FOLDER}codecov"
        
        REACT_ROOT_FOLDER = "${WORKSPACE}/Code/src/WeatherStationProject.Dashboard.App/ClientApp/"
        COVERAGE_ROOT_FOLDER_PATH="${REACT_ROOT_FOLDER}coverage/"
        
        DOTNET_COVERAGE_REPORT_PATH="${COVERAGE_ROOT_FOLDER_PATH}coverage.opencover.xml"
        REACT_COVERAGE_REPORT_PATH="${COVERAGE_ROOT_FOLDER_PATH}lcov.info"
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
                                  /d:sonar.testExecutionReportPaths="${COVERAGE_ROOT_FOLDER_PATH}test-report.xml" \
                                  /d:sonar.cs.opencover.reportsPaths="${DOTNET_COVERAGE_REPORT_PATH}" \
                                  /d:sonar.login=${SONAR_CREDENTIALS}
                           """
                        sh """
                           ( cd ${REACT_ROOT_FOLDER} && npm run test-coverage )
                           dotnet build ${WORKSPACE}/Code/WeatherStationProjectDashboard.sln
                           ( cd ${WORKSPACE}/Code && dotnet test WeatherStationProjectDashboard.sln \
                                                         "/p:CollectCoverage=true" \
                                                         "/p:CoverletOutput=${COVERAGE_ROOT_FOLDER_PATH}" \
                                                         "/p:CoverletOutputFormat=\"opencover\"" )
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
        
        stage('Upload report to Coveralls.io') {
            steps {
                withCredentials([string(credentialsId: 'coveralls-dashboard-repo-token',
                                        variable: 'COVERALLS_REPO_TOKEN')]) {
                    sh "${HOME}/.dotnet/tools/csmacnz.Coveralls --opencover -i ${DOTNET_COVERAGE_REPORT_PATH} --repoTokenVariable COVERALLS_REPO_TOKEN"
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
