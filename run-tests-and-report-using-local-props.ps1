<#
.SYNOPSIS
Run tests and generate coverage report using local MSBuild props file for environment settings.


.USAGE
From the solution root (where this script lives), run:
    ./run-tests-and-report-using-local-props.ps1
#>

# Go to script root
$ScriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $ScriptRoot

# Path to your local props file
$LocalPropsFile = Join-Path $ScriptRoot "PackageSettings.props.local"

if (-Not (Test-Path $LocalPropsFile)) {
    Write-Error "Local props file not found at $LocalPropsFile"
    exit 1
}

# Load MSBuild XML
[xml]$propsXml = Get-Content $LocalPropsFile

# Extract values and set env variables
$env:TELBLAZOR_PACKAGE_VERSION = $propsXml.Project.PropertyGroup.TELBlazorPackageVersion
$env:NUGET_PACKAGES_OUTPUT_PATH = $propsXml.Project.PropertyGroup.NugetPackagesOutputPath
$env:USE_TEL_BLAZOR_COMPONENTS_PROJECT_REFERENCE = $propsXml.Project.PropertyGroup.UseTELBlazorComponentsProjectReference
$env:DISABLE_PACKAGE_GENERATION = $propsXml.Project.PropertyGroup.DisablePackageGeneration

Write-Host "Using the following local settings from PackageSettings.Local.props:"
Write-Host "TELBLAZOR_PACKAGE_VERSION=$env:TELBLAZOR_PACKAGE_VERSION"
Write-Host "NUGET_PACKAGES_OUTPUT_PATH=$env:NUGET_PACKAGES_OUTPUT_PATH"
Write-Host "USE_TEL_BLAZOR_COMPONENTS_PROJECT_REFERENCE=$env:USE_TEL_BLAZOR_COMPONENTS_PROJECT_REFERENCE"
Write-Host "DISABLE_PACKAGE_GENERATION=$env:DISABLE_PACKAGE_GENERATION"

# Run Tests
dotnet test --no-build --no-restore --settings .runsettings

# Generate Coverage Report
dotnet reportgenerator `
    -reports:"**/AllTestResults/**/coverage.cobertura.xml" `
    -targetdir:CoverageReport `
    -reporttypes:Html

Write-Host "Coverage report generated at: $ScriptRoot\CoverageReport\index.html"
