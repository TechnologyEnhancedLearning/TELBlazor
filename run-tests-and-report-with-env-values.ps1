# run at solution level ./run-tests-and-report-with-env-values.ps1
# book mark in chrome file:///C:/dev/repos/TELBlazor/coveragereport/index.html
$env:TELBLAZOR_PACKAGE_VERSION = "10.9.9"
$env:NupkgOutputPath = "$PSScriptRoot\CICDPackageLocation"
$env:UseTELBlazorComponentsProjectReference = "true"
$env:TELBlazorPackageSource = "$PSScriptRoot\CICDPackageLocation"
$env:DisablePackageGeneration = "false"
$env:E2ETracingEnabled = "true"
$env:HeadlessTesting = "true"  # <-- this is what your test code uses
dotnet test --collect "XPlat Code Coverage" --settings "coverlet.runsettings"
# remove colon and added speach marks to the above
#next try
# dotnet test TELBlazor.Components.Tests/TELBlazor.Components.Tests.csproj ^
  # --collect "XPlat Code Coverage" ^
  # --settings "coverlet.runsettings" ^
  # -p:CollectCoverage=true
  # -p:IncludeTestAssembly=true

# dotnet test TELBlazor.Components.Tests/TELBlazor.Components.Tests.csproj --collect "XPlat Code Coverage" --settings "coverlet.runsettings" --p:IncludeTestAssembly=true

$env:TELBLAZOR_PACKAGE_VERSION = "10.9.9"
$env:NupkgOutputPath = "$PSScriptRoot\CICDPackageLocation"
$env:UseTELBlazorComponentsProjectReference = "true"
$env:TELBlazorPackageSource = "$PSScriptRoot\CICDPackageLocation"
$env:DisablePackageGeneration = "false"
$env:E2ETracingEnabled = "true"
$env:HeadlessTesting = "true"  # <-- this is what your test code uses
dotnet reportgenerator `
                -reports:"**/TestResults/**/coverage.cobertura.xml" `
                -targetdir:coveragereport `
                -reporttypes:Html  
				
