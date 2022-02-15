@Library('shared-library') _
import com.davidleonm.WeatherStationDashboardVariables
import com.davidleonm.GlobalVariables

pipeline {
    agent { label 'net-core-slave' }

    stages {
        stage('Prepare Python ENV') {
            steps {
                script {
                    prepareDashboardEnvironment()
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
                withCredentials([string(credentialsId: 'coveralls-dashboard-repo-token', variable: 'COVERALLS_REPO_TOKEN')]) {
                    sh 'coveralls'
                }
            }
        }

        stage('Deploy on production') {
            steps {
                script {
                    deployDashboardServices('',
                                            "${GlobalVariables.ProductionCredentialsDockerRegistryKey}",
                                            'Production')
                                            
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
