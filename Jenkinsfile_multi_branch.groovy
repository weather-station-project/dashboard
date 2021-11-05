@Library('shared-library') _
import com.davidleonm.WeatherStationDashboardVariables
import com.davidleonm.GlobalVariables

pipeline {
    agent { label 'net-core-slave' }

    environment {
        SONAR_CREDENTIALS = credentials('sonarqube-token')
        REACT_ROOT_FOLDER = "${WORKSPACE}/Code/src/WeatherStationProject.Dashboard.App/ClientApp"
    }

    stages {
        stage('Prepare ENV') {
            steps {
                script {
                    setBuildStatus('pending', "${WeatherStationDashboardVariables.RepositoryName}")
                    
                    println('Cleaning coverage report folder')
                    sh "rm -rf ${REACT_ROOT_FOLDER}/coverage"
    
                    println('Cleaning and preparing node_modules ENV')
                    sh """
                       rm -rf ${REACT_ROOT_FOLDER}/node_modules
                       ( cd ${REACT_ROOT_FOLDER} && npm install )
                       """
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
                                  /d:sonar.exclusions="**/*.spec.tsx,**/*.js,Code/tests/**/*.*" \
                                  /d:sonar.test.inclusions="**/*.spec.tsx,Code/tests/**/*.cs" \
                                  /d:sonar.typescript.lcov.reportPaths="${REACT_ROOT_FOLDER}/coverage/lcov.info" \
                                  /d:sonar.testExecutionReportPaths="${REACT_ROOT_FOLDER}/coverage/test-report.xml" \
                                  /d:sonar.login=${SONAR_CREDENTIALS}
                           """
                        sh """
                           ( cd ${REACT_ROOT_FOLDER} && npm run test-coverage )
                           dotnet build ${WORKSPACE}/Code/WeatherStationProjectDashboard.sln
                           dotnet test ${WORKSPACE}/Code
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

        stage('Deploy on staging') {
            // TODO DELETE THIS!!
            when {
                branch 'master'
            }
            stages {
                stage('Deploy Client App') {
                    steps {
                        script {
                            deployImageOnDockerRegistry("${GlobalVariables.StagingDockerRegistry}",
                                                        "${WeatherStationDashboardVariables.DashboardDockerRegistryName}",
                                                        "${GlobalVariables.StagingCredentialsDockerRegistryKey}",
                                                        "${getVersionFromCsproj('./Code/src/WeatherStationProject.Dashboard.App/WeatherStationProject.Dashboard.App.csproj')}",
                                                        './Dockerfile',
                                                        '--build-arg INCLUDE_NPM_ARG=true --build-arg PROJECT_NAME_ARG=WeatherStationProject.Dashboard.App --build-arg ENVIRONMENT_ARG=Staging')
                        }
                    }
                }

                stage('Deploy AirParametersService') {
                    steps {
                        script {
                            deployImageOnDockerRegistry("${GlobalVariables.StagingDockerRegistry}",
                                                        "${WeatherStationDashboardVariables.AirParametersServiceDockerRegistryName}",
                                                        "${GlobalVariables.StagingCredentialsDockerRegistryKey}",
                                                        "${getVersionFromCsproj('./Code/src/WeatherStationProject.Dashboard.AirParametersService/WeatherStationProject.Dashboard.AirParametersService.csproj')}",
                                                        './Dockerfile',
                                                        '--build-arg INCLUDE_NPM_ARG=false --build-arg PROJECT_NAME_ARG=WeatherStationProject.Dashboard.AirParametersService --build-arg ENVIRONMENT_ARG=Staging')
                        }
                    }
                }

                stage('Deploy AmbientTemperatureService') {
                    steps {
                        script {
                            deployImageOnDockerRegistry("${GlobalVariables.StagingDockerRegistry}",
                                                        "${WeatherStationDashboardVariables.AmbientTemperatureServiceDockerRegistryName}",
                                                        "${GlobalVariables.StagingCredentialsDockerRegistryKey}",
                                                        "${getVersionFromCsproj('./Code/src/WeatherStationProject.Dashboard.AmbientTemperatureService/WeatherStationProject.Dashboard.AmbientTemperatureService.csproj')}",
                                                        './Dockerfile',
                                                        '--build-arg INCLUDE_NPM_ARG=false --build-arg PROJECT_NAME_ARG=WeatherStationProject.Dashboard.AmbientTemperatureService --build-arg ENVIRONMENT_ARG=Staging')
                        }
                    }
                }

                stage('Deploy AuthenticationService') {
                    steps {
                        script {
                            deployImageOnDockerRegistry("${GlobalVariables.StagingDockerRegistry}",
                                                        "${WeatherStationDashboardVariables.AuthenticationServiceDockerRegistryName}",
                                                        "${GlobalVariables.StagingCredentialsDockerRegistryKey}",
                                                        "${getVersionFromCsproj('./Code/src/WeatherStationProject.Dashboard.AuthenticationService/WeatherStationProject.Dashboard.AuthenticationService.csproj')}",
                                                        './Dockerfile',
                                                        '--build-arg INCLUDE_NPM_ARG=false --build-arg PROJECT_NAME_ARG=WeatherStationProject.Dashboard.AuthenticationService --build-arg ENVIRONMENT_ARG=Staging')
                        }
                    }
                }

                stage('Deploy GatewayService') {
                    steps {
                        script {
                            deployImageOnDockerRegistry("${GlobalVariables.StagingDockerRegistry}",
                                                        "${WeatherStationDashboardVariables.GatewayServiceDockerRegistryName}",
                                                        "${GlobalVariables.StagingCredentialsDockerRegistryKey}",
                                                        "${getVersionFromCsproj('./Code/src/WeatherStationProject.Dashboard.GatewayService/WeatherStationProject.Dashboard.GatewayService.csproj')}",
                                                        './Dockerfile',
                                                        '--build-arg INCLUDE_NPM_ARG=false --build-arg PROJECT_NAME_ARG=WeatherStationProject.Dashboard.GatewayService --build-arg ENVIRONMENT_ARG=Staging')
                        }
                    }
                }

                stage('Deploy GroundTemperatureService') {
                    steps {
                        script {
                            deployImageOnDockerRegistry("${GlobalVariables.StagingDockerRegistry}",
                                                        "${WeatherStationDashboardVariables.GroundTemperatureServiceDockerRegistryName}",
                                                        "${GlobalVariables.StagingCredentialsDockerRegistryKey}",
                                                        "${getVersionFromCsproj('./Code/src/WeatherStationProject.Dashboard.GroundTemperatureService/WeatherStationProject.Dashboard.GroundTemperatureService.csproj')}",
                                                        './Dockerfile',
                                                        '--build-arg INCLUDE_NPM_ARG=false --build-arg PROJECT_NAME_ARG=WeatherStationProject.Dashboard.GroundTemperatureService --build-arg ENVIRONMENT_ARG=Staging')
                        }
                    }
                }

                stage('Deploy RainfallService') {
                    steps {
                        script {
                            deployImageOnDockerRegistry("${GlobalVariables.StagingDockerRegistry}",
                                                        "${WeatherStationDashboardVariables.RainfallServiceDockerRegistryName}",
                                                        "${GlobalVariables.StagingCredentialsDockerRegistryKey}",
                                                        "${getVersionFromCsproj('./Code/src/WeatherStationProject.Dashboard.RainfallService/WeatherStationProject.Dashboard.RainfallService.csproj')}",
                                                        './Dockerfile',
                                                        '--build-arg INCLUDE_NPM_ARG=false --build-arg PROJECT_NAME_ARG=WeatherStationProject.Dashboard.RainfallService --build-arg ENVIRONMENT_ARG=Staging')
                        }
                    }
                }

                stage('Deploy WindMeasurementsService') {
                    steps {
                        script {
                            deployImageOnDockerRegistry("${GlobalVariables.StagingDockerRegistry}",
                                                        "${WeatherStationDashboardVariables.WindMeasurementsServiceDockerRegistryName}",
                                                        "${GlobalVariables.StagingCredentialsDockerRegistryKey}",
                                                        "${getVersionFromCsproj('./Code/src/WeatherStationProject.Dashboard.WindMeasurementsService/WeatherStationProject.Dashboard.WindMeasurementsService.csproj')}",
                                                        './Dockerfile',
                                                        '--build-arg INCLUDE_NPM_ARG=false --build-arg PROJECT_NAME_ARG=WeatherStationProject.Dashboard.WindMeasurementsService --build-arg ENVIRONMENT_ARG=Staging')
                        }
                    }
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
