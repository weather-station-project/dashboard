@Library('shared-library') _
import com.davidleonm.WeatherStationDashboardVariables
import com.davidleonm.GlobalVariables

pipeline {
    agent { label 'net-core-slave' }
    
    environment {
        REACT_ROOT_FOLDER = "${WORKSPACE}/Code/src/WeatherStationProject.Dashboard.App/ClientApp"
        DOTCOVER_FOLDER = "${WORKSPACE}/tools"
        DOTCOVER_PATH = "${DOTCOVER_FOLDER}/dotnet-dotcover"
    }

    stages {
        stage('Prepare Python ENV') {
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

                    println('Cleaning and installing dotCover')
                    sh """
                       rm -rf ${DOTCOVER_FOLDER}
                       dotnet tool install JetBrains.dotCover.GlobalTool --no-cache --tool-path ${DOTCOVER_FOLDER}
                       """
                }
            }
        }

        stage('Execute unit tests and code coverage') {
            steps {
                script {
                    sh """
                       ( cd ${REACT_ROOT_FOLDER} && npm run test-coverage )
                       dotnet build ${WORKSPACE}/Code/WeatherStationProjectDashboard.sln
                       ( cd ${WORKSPACE}/Code && ${DOTCOVER_PATH} test --no-build \
                                                                       --dcReportType=HTML \
                                                                       --dcOutput="${REACT_ROOT_FOLDER}/coverage/dotnet-coverage.html" )
                       """
                }
            }
        }

        stage('Upload report to Coveralls.io') {
            steps {
                withCredentials([string(credentialsId: 'coveralls-sensors-reader-repo-token', variable: 'COVERALLS_REPO_TOKEN')]) {
                    sh 'ENV/bin/coveralls'
                }
            }
        }

    stage('Build & Deploy image') {
      steps {
        script {
          deployContainerOnDockerHub("${WeatherStationSensorsReaderVariables.DockerHubRegistryName}")
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
