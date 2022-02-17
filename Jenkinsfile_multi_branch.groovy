@Library('shared-library') _
import com.davidleonm.WeatherStationDashboardVariables
import com.davidleonm.GlobalVariables

pipeline {
    agent { label 'net-core-slave' }

    environment {
        SONAR_CREDENTIALS = credentials('sonarqube-token')
        
        REACT_ROOT_FOLDER = "${WORKSPACE}/Code/src/WeatherStationProject.Dashboard.App/ClientApp"
        TOOLS_FOLDER = "${WORKSPACE}/tools"
        DOTCOVER_PATH = "${HOME}/.dotnet/tools/dotnet-dotcover"
        CODECOV_PATH = "${TOOLS_FOLDER}/codecov"
        
        DOTNET_COVERAGE_REPORT_PATH="${REACT_ROOT_FOLDER}/coverage/dotnet-coverage.xml"
        REACT_COVERAGE_REPORT_PATH="${REACT_ROOT_FOLDER}/coverage/lcov.info"
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
                                  /d:sonar.testExecutionReportPaths="${REACT_ROOT_FOLDER}/coverage/test-report.xml" \
                                  /d:sonar.cs.dotcover.reportsPaths="${DOTNET_COVERAGE_REPORT_PATH}" \
                                  /d:sonar.login=${SONAR_CREDENTIALS}
                           """
                        sh """
                           ( cd ${REACT_ROOT_FOLDER} && npm run test-coverage )
                           dotnet build ${WORKSPACE}/Code/WeatherStationProjectDashboard.sln
                           ( cd ${WORKSPACE}/Code && ${DOTCOVER_PATH} test --no-build \
                                                                           --dcReportType=XML \
                                                                           --dcOutput="${DOTNET_COVERAGE_REPORT_PATH}" )
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
                    deployDashboardServices("${GlobalVariables.StagingDockerRegistry}",
                                            "${GlobalVariables.StagingCredentialsDockerRegistryKey}",
                                            'Staging')
                                            
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
