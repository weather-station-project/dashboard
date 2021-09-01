@Library('shared-library') _
import com.davidleonm.WeatherStationDashboardVariables
import com.davidleonm.GlobalVariables

pipeline {
    agent { label 'net-core-slave' }

    environment {
        SONAR_CREDENTIALS = credentials('sonarqube-token')
    }

    stages {
    /*stage('Prepare Python ENV') {
      steps {
        script {
          setBuildStatus('pending', "${WeatherStationDashboardVariables.RepositoryName}")

          // Clean & Prepare new python environment
          sh '''
             rm -rf ENV
             python3 -m venv ENV

             ENV/bin/pip install --no-cache-dir --upgrade pip
             ENV/bin/pip install --no-cache-dir --upgrade wheel
             ENV/bin/pip install --no-cache-dir --upgrade setuptools

             ENV/bin/pip install --no-cache-dir psycopg2 gpiozero coverage
             '''
        }
      }
    }*/

    /*
    stage('Execute unit tests and code coverage') {
      steps {
        script {
          sh """
             ENV/bin/python -m unittest discover -s ${WORKSPACE}/WeatherStationSensorsReader
             ENV/bin/coverage run -m unittest discover -s ${WORKSPACE}/WeatherStationSensorsReader
             """

          sh "dotnet test ${WORKSPACE}/Code"
        }
      }
    }
    */

        stage('SonarQube analysis') {
            environment {
                def scannerHome = tool 'Sonarqube'
            }

            steps {
                /*script {
                    sh "ENV/bin/coverage xml"
                }*/

                withSonarQubeEnv('Sonarqube') {
                    sh """
                       dotnet ${scannerHome}/SonarScanner.MSBuild.dll begin /k:Dashboard /d:sonar.login=${SONAR_CREDENTIALS}
                       dotnet build ${WORKSPACE}/Code/WeatherStationProjectDashboard.sln
                       dotnet ${scannerHome}/SonarScanner.MSBuild.dll end /d:sonar.login=${SONAR_CREDENTIALS}
                       """
                }

                timeout(time: 10, unit: 'MINUTES') {
                    waitForQualityGate abortPipeline: true
                }
            }
        }

        stage('Deploy App on staging') {
            steps {
                script {
                    deployImageOnDockerRegistry("${GlobalVariables.StagingDockerRegistry}",
                                                "${WeatherStationDashboardVariables.DockerRegistryName}",
                                                "${GlobalVariables.StagingCredentialsDockerRegistryKey}",
                                                '1.0.0',
                                                './Dockerfile',
                                                '--build-arg INCLUDE_NPM=true --build-arg PROJECT_NAME=WeatherStationProject.Dashboard.App')
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
