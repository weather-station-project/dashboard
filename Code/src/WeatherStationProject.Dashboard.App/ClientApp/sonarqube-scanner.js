const scanner = require('sonarqube-scanner');

scanner(
    {
        serverUrl: "http://192.168.1.69:9001",
        token:"548d64019fc50f8637c43746abd1abd8430c27e1",
        options: {
            "sonar.sources": "src",
            "sonar.exclusions": "**/*.spec.tsx",
            "sonar.tests": "src",
            "sonar.test.inclusions": "**/*.spec.tsx",
            "sonar.typescript.lcov.reportPaths": "coverage/lcov.info",
            "sonar.testExecutionReportPaths": "coverage/test-report.xml"
        },
    },
    () => process.exit()
);