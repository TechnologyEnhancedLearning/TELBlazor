# TELBlazor
TEL Blazor Component Library Package

# This ReadMe

This readme is the repo readme, it is also used as the package readme as this is the solution for making the TELBlazor.Components package.
There is also a cicd readme in the workflow folder.

The [MVCBlazor repo ](https://github.com/TechnologyEnhancedLearning/MVCBlazor) readme has exploration of various of the choices and alternatives to what is implemented here, which may be useful along with the other blazor repos, when developing for this repo.

*There are specific readmes within the solution for topics like components and cicd*

# Purpose

Progressive Blazor components, intended for a Hosted Blazor Webassembly architecture with prerendering. They provide clientside functionality with 
the ability to produce static prerendered html. The prerendered html is written to have server-side interactvity via form posts and fallback mechanism to support browsers without javascript enabled. 
  
   
# Links

[Bottom file see tree diagram of solution](#Project-Structure-and-Comments)

[Easy viewing of TELBlazor Repo ReadMe](https://raw.githubusercontent.com/TechnologyEnhancedLearning/TELBlazor/refs/heads/master/README.md)
(best with Markdown Reader Chrome extension)

[Last Published Package](https://github.com/orgs/TechnologyEnhancedLearning/packages?tab=packages&q=TELBlazor)

[TELBlazor](https://github.com/TechnologyEnhancedLearning/TELBlazor)

[TELBlazor Production Showcase](https://technologyenhancedlearning.github.io/TELBlazor/)

[TELBlazor Dev Showcase](https://technologyenhancedlearning.github.io/TELBlazor-DevShowCase/)

[View Packaged Dev Showcase Code](https://github.com/TechnologyEnhancedLearning/TELBlazor-DevShowCase/tree/gh-pages)

[View Packaged Showcase Code](https://github.com/TechnologyEnhancedLearning/TELBlazor/tree/gh-pages/)

[Code Report Page](https://technologyenhancedlearning.github.io/TELBlazor-CodeReport/)

[NHSE TEL Frontend](https://github.com/TechnologyEnhancedLearning/nhse-tel-frontend)

# Set Up

## Prerequisites

- **Visual Studio 2022** (Community or higher)
- **.NET 8 SDK version 8.0.407 or later** (see global.json file in the solution)
- **Node.js 18+** and npm
- **Git** configured with your credentials
- **PowerShell 5.1+** 
> ⚠️ **Important:** All commands in this guide require **PowerShell running as Administrator**

### Quick Setup ⚡


- **Get the repo:** open powershell as admin, ```cd``` to where you keep your repos. Run ```git clone https://github.com/TechnologyEnhancedLearning/TELBlazor.git```
- **Create local gitignored configuration files from templates:** open the project in Visual Studio
	- search template
		- create a copy of each template removing the '.template' post fix
- **Create GitHub Personal Access Token (PAT) for consuming [TEL git hosted nuget packages](https://github.com/orgs/TechnologyEnhancedLearning/packages) :**
	- go to your [GitHub profile tokens section](https://github.com/settings/tokens)
	- Select**Personal access tokens** → **Tokens classic** → **Generate new token (classic)**
		- give it a Note, e.g. 'TEL Package Access' (we will be storing it as the env var 'TEL_GIT_PACKAGES_TOKEN')
		- set the expiration date to something reasonable (e.g. 90 days)
		- select the scopes you need for the token
			- as a minimum select `read:packages` and you may wish to increase the expiration date.
			- if you want to publish packages you will need `write:packages` and `delete:packages` but this is not recommended for development
		- generate token
		- copy the token ***immediately*** (it will not be accessible again)
	- **Set system wide environment variables (useful for future projects too):**
		- open Start, type 'Environment Variables', and select **Edit the system environment variables**
		- in the **System Properties** window, select **Environment Variables**
		- under **System variables**, select **New**
		- enter the variable names and variable values as follows:
			- GITHUB_USERNAME / [Your GitHub username]
			- TEL_GIT_PACKAGES_TOKEN / [The copied token]
		- then select **OK**
		- select **OK** again to close all dialogs.
- **Restore Nuget, Npm, Tooling, playwright and build:**
	- run a new admin powershell terminal in the TELBlazor repo root
	- run the following commands
	```   
		#1. Restore
		dotnet clean
		dotnet restore
		
		#2. Nuget TEL Git Feed setups
		dotnet nuget add source "https://nuget.pkg.github.com/TechnologyEnhancedLearning/index.json" `
		  --name "github" `
		  --username $env:GITHUB_USERNAME `
		  --password $env:TEL_GIT_PACKAGES_TOKEN `
		  --store-password-in-clear-text

		# 3. Restore .NET CLI tools (Playwright, report manager for code coverage)
		Write-Output "Restore Tools"
		dotnet tool restore

		# 4. Install Node dependencies (gulp, playwright, frontend libs)
		Write-Output "Restore Node"
		npm install
		
		# 5. Build solution or run other commands as needed
		Write-Output "Build solution without build package, using project references instead of local package or remote package"
		dotnet build
		
		# 6. Setup playwright
		Write-Output "Playwright setup"
		& ".\TELBlazor.Components.ShowCase.E2ETests\bin\Debug\net8.0\playwright.ps1" install
	```

The project should now work. See other sections for what projects to run, and configuration.

> ⚠️ read the contribution section before creating a branch or commits ⚠️


### Getting Started with the Project following Setup

It is recommended you check setup by reading this section and making sure packages can be created and consumed and all tests pass.

#### Contributing to the project

- [Branch naming rules](https://github.com/TechnologyEnhancedLearning/TELBlazor/blob/master/.releaserc.json)
- ⚠️ commit names are used to generate the package version, repo version and change log so much be correct ⚠️[Commit Naming Rules](https://github.com/TechnologyEnhancedLearning/TELBlazor/blob/master/.commitlintrc.json)
	- e.g. 
		> 'docs(readme): added detail on commit rules'
[![semantic-release: angular](https://img.shields.io/badge/semantic--release-angular-e10079?logo=semantic-release)](https://github.com/semantic-release/semantic-release)
[Commit conventions](https://github.com/angular/angular/blob/main/contributing-docs/commit-message-guidelines.md)

#### Configuring the project

| File | Description | Variable | Purpose|
|---|---|---|---|
|runsettings|configures playwrights| headless, tracing| make tests more detailed or visible or run faster|
|nuget.config|overwritten in cicd so feel free to change it| Remote and local telblazor nuget sources |Comment in/out depending which source you need|	
|Package.Settings.Props | Used by CICD can receive environment variables or msbuild parameters | 	NugetPackagesOutputPath  UseTELBlazorComponentsProjectReference  TELBlazorPackageVersion DisablePackageGeneration | For CICD |
|Package.Settings.Props.local| overwrite package.settings.props locally | NugetPackagesOutputPath | Where you want your package creating |
|Package.Settings.Props.local| overwrite package.settings.props locally | UseTELBlazorComponentsProjectReference | For quick development use the project reference instead of a package |
|Package.Settings.Props.local| overwrite package.settings.props locally| TELBlazorPackageVersion | local package version you're creating or consuming |
|Package.Settings.Props.local|overwrite package.settings.props locally | DisablePackageGeneration | Stops package creation on build. |
|appsettings | | | Serilog configuration |

> **Nb.** It is recommended not to build the solution with package generation enabled and project reference disabled at the same time.
> **Nb.** Creating your package outside of the project can be convenient for other local solutions consuming it.



#### Running the projects
- depending on whether your consuming a package or using a project reference set package.settings.props.local UseTELBlazorComponentsProjectReference to what you want
- run wasmserverhost to see the showcase as blazor wasm prerendered
- run wasmstaticclient to see pure wasm site

#### Building the package
- to build a package
	- nuget config
		- ensure the feed is still set to local```<add key="TELBlazorPackageSource" value="%LOCAL_PACKAGES_PATH%" />```
	- **package.settings.props.local** to create package
		- NugetPackagesOutputPath -> 'LOCAL_PACKAGES_PATH'
		- UseTELBlazorComponentsProjectReference -> 'true'
		- TELBlazorPackageVersion -> '9.9.9-local' or something higher than you have
		- DisablePackageGeneration -> 'false'
	- delete **.lock** files
	- build solution
	- change **package.settings.props.local** to consume package
		- UseTELBlazorComponentsProjectReference -> 'false'
		- DisablePackageGeneration -> 'true'
	- build solution



#### Testing the project
- you can use the test runner
- **runsettings** allows configuration of headless and tracing
- **packagesettings.props** sets thresholds
- running at solution level **./run-tests-and-report-with-env-values.ps1** (see in the file for specific arguments you may want to set)
	- will produce a test coverage site **[your repo folder]/TELBlazor/CoverageReport/index.html**
	- then look at CoverageReport in solution root and you can bookmark index.html to see your coverage
	- E2E project has a Reports folder for tracings
- Similarly run-tests-and-report-using-local-props.ps1 runs just using your local props file
- appsettings.Development can be set in the E2E Host to log to the Logs folder at project route, see template


#### Tips

- If you don't want to wait for the pipeline to fail your commit names and for pushes to accidently expose your secrets: you may want to add
	- gitguardian from confluence docs (follow it to the letter) [gitguardian global setup instructions](https://hee-tis.atlassian.net/wiki/spaces/TP/pages/3855253505/GitGuardian+Setup+-+Simplified+Version)
	- and add a pre-commit and push hook (you need both as you cannot lint what hasn't yet been commit) you can add these to your git templates if you want them for every repo, or just to this repos pre- push- commits, or you can be lazy and add them into the gitguardian hook
	- you will need git commitlint globally (angular because it goes with the versioning tool for repo versioning)
		
		```
			npm install -g @commitlint/cli @commitlint/config-angular
		```
	-	You can put into your pre-push, pre-commit hook, commit-ms, or into the gitguardian hooks some logic like the below.
	```
			#### --- Commitlint Logic (force local config) ---
			REPO_ROOT=$(git rev-parse --show-toplevel)
			CONFIG_PATH="$REPO_ROOT/.commitlintrc.json"
			echo "COMMIT_MSG_FILE is one msg behind it is only set to the commit your trying to commit after the commit succeeds so this script is one behind"
			echo "so would work best in prepush template"
			echo "To fix squash out the commit following renaming and renaming commit_editmsg and if pushed to remote git push --force"
			COMMIT_MSG_FILE="$REPO_ROOT/.git/COMMIT_EDITMSG"

			# Check if the local .commitlintrc.json exists in the repository root
			if [ -f "$CONFIG_PATH" ]; then

				echo "✅ Local .commitlintrc.json found at: $CONFIG_PATH. Running commitlint..."
				echo "⚠️ if commit lint fails in precommit its actually reading previous commit name you need to reset or squash make a new commit and change .git/COMMIT_EDITMSG to something that would pass. ⚠️"

				if command -v npx &> /dev/null; then
				
						cd "$REPO_ROOT" || exit 1

						# Debug: Print the commit message file content
						if [ -f "$COMMIT_MSG_FILE" ]; then
							echo "Commit message content:"
							cat "$COMMIT_MSG_FILE"
						else
							echo "❌ Commit message file not found!"
							exit 1
						fi
						
						
						# Run commitlint, explicitly pointing to the local config and providing the commit message via stdin
						OUTPUT1=$(npx.cmd --no -- commitlint --config=".commitlintrc.json" --edit="$COMMIT_MSG_FILE" 2>&1)
						
						EXIT_CODE1=$?
						# echo "!!!!!!!!!!! OUTPUT1"
						# echo "$OUTPUT1"
						
						
						# OUTPUT2=$(npx.cmd --no -- commitlint --config=".commitlintrc.json" --from=origin/master --to=HEAD 2>&1)
						
						# EXIT_CODE2=$?
						# echo "!!!!!!!!!!! OUTPUT2"
						# echo "$OUTPUT2"
						
						# OUTPUT3=$(npx.cmd --no -- commitlint --config=".commitlintrc.json"  --from-last-tag 2>&1)
						
						
						# EXIT_CODE3=$?
						# echo "!!!!!!!!!!! OUTPUT3"
						# echo "$OUTPUT3"
						


					#if [ "$EXIT_CODE1" -ne 0 ] || [ "$EXIT_CODE2" -ne 0 ] || [ "$EXIT_CODE3" -ne 0 ];  then
					if [ "$EXIT_CODE1" -ne 0 ];  then
						echo "❌ Commitlint failed:"
						
						echo "$OUTPUT1"
						exit 1
					else
						echo "✅ Commitlint passed!"
					fi
				else
					echo "⚠️ npx not found. Please ensure Node.js and npm are installed to use commitlint."
					# Optionally fail here
					# exit 1
				fi
			else
				echo "ℹ️ No local .commitlintrc.json found in $REPO_ROOT. Skipping commitlint."
			fi
		``` 

### Troubleshooting 
#### Package build errors 
- delete local **TELBlazor.Components** packages
- check **TELBlazorPackageVersion** has been incremented
- delete **.lock** files
- clean the solution
- check environment values in **props** and **nuget.config**
- restore nuget packages 
- restore the solution
- if still not working delete **bin** and **obj**
- if still not working, restart Visual Studio
- if there are still issues its easier to problem solve by using a random very high **TELBlazor.Components** package version number and ensuring it fails and says it found the source but not the version

#### Git commit names
- git commit names can be caught locally
- if they are not
	- **fetch**, **pull** and **squash** before the change and then `git push origin <your_branch_name> --force`


# How to consume TELBlazor.Components
1. select a production version of the package [Package list for TELBlazor.Component on git](https://github.com/TechnologyEnhancedLearning/TELBlazor/pkgs/nuget/TELBlazor.Components)
1. set up CSS references and dependency injection using lean host examples WasmServerHost, WasmServerHost.Client and WasmStaticClient
from the repo and ShowCase project for how to include the package. 
1. you will need a copy of nhsuk.css and a reference <link href="css/nhsuk.css" rel="stylesheet" /> see gulp in the previously mentioned projects
1. see setup notes for instructions on getting a git nuget token for consuming the package.

# Solution and Pipeline

## Features of CICD
- there is a readme in the CICD
- a DevShowCase site is created using a DevPackage and the same in production
	- the dev pipeline also publishes a coverage report


# Solution Detail

## Architecture

### Project Structures used
- Repo TELBlazor

 - TELBlazor.Components
	- this is a razor component library
 - TELBlazor.Components.UnitTests
	- Bunit template from bunit site, configured to use Xunit
 - TELBlazor.Components.ShowCase.E2ETests
    - NUnit Playwright Test project
 - TELBlazor.Components.ShowCase.Shared
	- this is a razor component library
 - TELBlazor.Components.ShowCase.E2ETests.WasmServerHost
	- Wasm host
 - TELBlazor.Components.ShowCase.E2ETests.WasmServerHost.Client 
	- Wasm hosted
 - TELBlazor.Components.ShowCase.WasmStaticClient
	- Wasm standalone 
	


### Logging
- For more detailed dependency injected logging see MVCBlazor project	
	
## Notes

### Stuff you don't need to know (but may be useful for a specific issue on searching the readme)
- it is not render auto per components because the intention is to be used in MVC views.
- Xunit is used with Bunit and Nunit with playwright, either could be 
changed so that they are using the same and this could be done in future 
as the libraries improve but currently each is being used with the 
recommend tool it is designed for though both support the others tool.
- not using data-testid="TELButton" because we should use aria selectors. We may change this later (first rule of aria dont use aria).
   - e.g. data-attribute-telblazorcomponentname="TelBlazorButton"
- not using guid id as i have elsewhere either [Parameter] public string ElementId { get; set; } = $"tel-button-{Guid.NewGuid():N}";
- various things have been cut from mvcblazor so it is worth returning to for potential solutions if developing this solution [MVCBlazor repo](https://github.com/TechnologyEnhancedLearning/MVCBlazor)


# Project Structure and Comments

Viewed best [raw ReadMe see chrome](https://raw.githubusercontent.com/TechnologyEnhancedLearning/TELBlazor/refs/heads/master/README.md)

|  Description  | File Structure |
|----------------|-------------|
|  | &#9507; TELBlazor |
| Test coverage from cicd or .ps1 | &#160;&#160;&#160;&#160;&#9507; AllTestResults |
| Convenient in solution location | &#160;&#160;&#160;&#160;&#9507; CICDPackageLocation |
| Test report site from cicd or .ps1 viewably locally or [Dev Report](https://technologyenhancedlearning.github.io/TELBlazor-CodeReport/) | &#160;&#160;&#160;&#160;&#9507; CoverageReport |
| gh-pages site publish folder | &#160;&#160;&#160;&#160;&#9507; docs |
| Sets centralised solution a report thresholds | &#160;&#160;&#160;&#160;&#9507; Directory.Build.props,  |
| Centralised nuget packages | &#160;&#160;&#160;&#160;&#9507;  Directory.Packages.props |
| CICD setting .net version | &#160;&#160;&#160;&#160;&#9507; global.json |
| Uses environment variables to set packed package location locally or remote | &#160;&#160;&#160;&#160;&#9507; nuget.config|
| Centralised npm packages used with gulp and for testing | &#160;&#160;&#160;&#160;&#9507; package.json |
| Linting config for branch and commit names | &#160;&#160;&#160;&#160;&#9507; .releaserc.json, .commitlintrc.json |
| Useful for running tests and generating test report locally | &#160;&#160;&#160;&#160;&#9507;  run-tests-and-report-with-env-values.ps1 |
| Env Setup | &#160;&#160;&#160;&#160;&#9507; PackageSettings.props |
| Env Setup, testing, package or project reference  | &#160;&#160;&#160;&#160;&#9507; PackageSettings.props.local |
| The package create on build | &#160;&#160;&#160;&#160;&#9507; TELBlazor.Components |
| All using in imports so can add components by assembly | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; _Imports.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; Components |
| Standard components made to TEL and NoJS requirements | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; BaseComponents |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; TELButton.razor |
| Only to support main component TELButton | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; TELButtonSubComponent.razor |
| Useful components for development | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; TestComponents |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; Core |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; Compliance |
| Create a contract of accessibility | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; IAccessibleComponent.cs |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; Configuration |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; ITELBlazorBaseComponentConfiguration.cs |
| Logging, NoJS bool  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; TELBlazorBaseComponentConfiguration.cs |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; DI |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; Enums |
| Enum of css styles for testing and setting component | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; TELButtonStyle.cs |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; Services |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; HelperServices |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; ILogLevelSwitcherService.cs |
| Custom component base with configuration injected | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; TELComponentBase.cs |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; TELBlazorPackageVersion |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; TELBlazorPackageVersionInformation.razor |
| Programmatically generate via gulp csproj package versions | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; VersionInfo.cs |
|  | &#160;&#160;&#160;&#160;&#9507; TELBlazor.Components.ShowCase.E2ETests |
| Enables run blazor pages in E2E | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; BlazeWright |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; BlazorApplicationFactory.cs |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; BlazorPageExtensions.cs |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; BlazorPageTest.cs |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; Helpers |
| Enables using attributes to run same tests with different viewports and browsers | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; BrowserHelper.cs |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; ViewportHelper.cs |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; Pages |
| Structure to mirror ShowCase | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; BaseComponentPages |
| Check NoJs, in view, functionality | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; TELButtonPageTests.cs |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; ComponentPages |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; TestComponentPages |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; Reports |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; SnapShots |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; TestDoubles |
| Serverside so can have prerender behaviour for nojs lean host | &#160;&#160;&#160;&#160;&#9507; TELBlazor.Components.ShowCase.E2ETests.WasmServerHost |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; TELBlazor.Components.ShowCase.E2ETests.WasmServerHost |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; _Imports.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; App.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; Program.cs |
| Clientside for the host | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; TELBlazor.Components.ShowCase.E2ETests.WasmServerHost.Client |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; _Imports.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; App.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; Program.cs |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; Routes.razor |
| ShowCase is an example of implementation also provides site for E2E and showcase | &#160;&#160;&#160;&#160;&#9507; TELBlazor.Components.ShowCase.Shared |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; _Imports.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; DI |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; Layouts |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; ComponentLayouts |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; ComponentNavMenu.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; ComponentPageLayout.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; ShowCase.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; MainLayout.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; Pages |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; ComponentPages |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; BaseComponentPages |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; TELButtonPage.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; TestComponentPages |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; CssSourceCheckerPage.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; LogLevelSwitcherPage.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; Error.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; Home.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; Services |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; HelperServices |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; SerilogLogLevelSwitcher.cs |
| Published to create gh-pages [DevShowCase](https://technologyenhancedlearning.github.io/TELBlazor-DevShowCase/) [ShowCase](https://technologyenhancedlearning.github.io/TELBlazor/) | &#160;&#160;&#160;&#160;&#9507; TELBlazor.Components.ShowCase.WasmStaticClient |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; _Imports.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; App.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; Program.cs |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; Routes.razor |
|  | &#160;&#160;&#160;&#160;&#9507; TELBlazor.Components.UnitTests |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; _Imports.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; Components |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; BaseComponents |
| Axe and functionality test, does not simulate nojs or browser | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; TELButtonTests.razor |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; TestComponents |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; Core |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; DI |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; DI.cs |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; FallbackServiceProvider.cs |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9507; HtmlComparisons |
|  | &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#9495; TestDoubles |