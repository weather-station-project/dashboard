@Library('shared-library') _
import com.davidleonm.WeatherStationDashboardVariables
import com.davidleonm.GlobalVariables

pipeline {
    agent { label 'net-core-slave' }

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
                    sh "${scannerHome}/sonar-scanner-4.6.1.2450/bin/sonar-scanner"
                }

                timeout(time: 10, unit: 'MINUTES') {
                    waitForQualityGate abortPipeline: true
                }
            }
        }

        stage('Deploy on staging') {
            steps {
                script {
                    deployImageOnDockerRegistry("${GlobalVariables.StagingDockerRegistry}",
                                                "${WeatherStationDashboardVariables.DockerRegistryName}",
                                                "${GlobalVariables.StagingCredentialsDockerRegistryKey}",
                                                '1.0.0',
                                                './Dockerfile')
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
