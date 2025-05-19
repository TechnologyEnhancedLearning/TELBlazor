# run at solution level ./run-tests-and-report-with-env-values.ps1
# book mark in chrome file:///C:/dev/repos/TELBlazor/coveragereport/index.html
# or use nektos act on the specific job to locally run the pipeline job
$env:TELBLAZOR_PACKAGE_VERSION = "10.9.9"
$env:NupkgOutputPath = "$PSScriptRoot\CICDPackageLocation"
$env:UseTELBlazorComponentsProjectReference = "true"
$env:TELBlazorPackageSource = "$PSScriptRoot\CICDPackageLocation"
$env:DisablePackageGeneration = "false"
$env:E2ETracingEnabled = "true"
$env:HeadlessTesting = "true"

# Run Tests
dotnet test


# Create Report from Test outputs
dotnet reportgenerator `
                -reports:"**/TestResults/**/coverage.cobertura.xml" `
                -targetdir:CoverageReport `
                -reporttypes:Html  
				
