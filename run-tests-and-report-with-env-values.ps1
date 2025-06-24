# run at solution level ./run-tests-and-report-with-env-values.ps1
# book mark in chrome file:///C:/dev/repos/TELBlazor/coveragereport/index.html
# or use nektos act on the specific job to locally run the pipeline job
$env:TELBLAZOR_PACKAGE_VERSION = "10.9.9"
$env:NUGET_PACKAGES_OUTPUT_PATH = "$PSScriptRoot\CICDPackageLocation"
$env:USE_TEL_BLAZOR_COMPONENTS_PROJECT_REFERENCE = "true"
$env:DISABLE_PACKAGE_GENERATION = "false"


# Run Tests
dotnet test --no-build --no-restore --settings .runsettings


# Create Report from Test outputs
dotnet reportgenerator `
                -reports:"**/AllTestResults/**/coverage.cobertura.xml" `
                -targetdir:CoverageReport `
                -reporttypes:Html  
				
