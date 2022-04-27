@Library('shared-library') _
import com.davidleonm.WeatherStationDashboardVariables
import com.davidleonm.GlobalVariables

pipeline {
    agent { label 'net-core-slave' }
    
    environment {
        TOOLS_FOLDER = "${WORKSPACE}/tools/"
        CODECOV_PATH = "${TOOLS_FOLDER}codecov"
        
        REACT_ROOT_FOLDER = "${WORKSPACE}/Code/src/WeatherStationProject.Dashboard.App/ClientApp/"
        COVERAGE_ROOT_FOLDER_PATH="${REACT_ROOT_FOLDER}coverage/"
        
        DOTNET_COVERAGE_REPORT_PATH="${COVERAGE_ROOT_FOLDER_PATH}coverage.opencover.xml"
        REACT_COVERAGE_REPORT_PATH="${COVERAGE_ROOT_FOLDER_PATH}lcov.info"
    }

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
                       ( cd ${WORKSPACE}/Code && dotnet test WeatherStationProjectDashboard.sln \
                                                     "/p:CollectCoverage=true" \
                                                     "/p:CoverletOutput=${COVERAGE_ROOT_FOLDER_PATH}" \
                                                     "/p:CoverletOutputFormat=\"opencover\"" )
                       """
                }
            }
        }

        stage('Upload report to Codecov') {
            steps {
                withCredentials([string(credentialsId: 'codecov-dashboard-token',
                                        variable: 'CODECOV_TOKEN')]) {
                    sh """
                       ${CODECOV_PATH} -y codecov.yml -t ${CODECOV_TOKEN} -f ${DOTNET_COVERAGE_REPORT_PATH} -F net
                       ${CODECOV_PATH} -y codecov.yml -t ${CODECOV_TOKEN} -f ${REACT_COVERAGE_REPORT_PATH} -F react
                       """
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
