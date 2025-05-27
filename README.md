# TELBlazor
TEL Blazor Component Library Package

# This ReadMe

This readme is the repo readme, it is also used as the package readme as this is the solution for making the TELBlazor.Components package.
There is also a cicd readme in the workflow folder.

The [MVCBlazor repo ](https://github.com/TechnologyEnhancedLearning/MVCBlazor) readme has exploration of various of the choices and alternatives to what is implemented here. And the other blazor repos may
also be a useful resource when developing for this repo.

# Purpose

Progressive components, that use the server prerendering in Global Wasm Blazor to ensure that if the user has no JS they will get html. And that html can be created to have working post actions.
The render cycle will hydrate the prerender and the post actions will be overrided by services injected in the components.
It is client side so the users browser will do the work.
  
  
   
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

## "It works on my machine"
qqqq run release first make branch for kevin
### Hi Kevin, Please rewrite or correct these steps. 
- im expecting ome global node may end up not working if anything as its already set up on my machine

### Steps

#### Get the repo
1. Go to [TELBlazor Repo](https://github.com/TechnologyEnhancedLearning/TELBlazor) hit code and get the clone string
1. Clone the repo locally
1. Go to [TELBlazor Repo](https://github.com/TechnologyEnhancedLearning/TELBlazor) 
	- create a branch (**there is branch name checks in cicd**) so for example call it "docs-readme-setup-instructions"
	- [branch lint rule](https://github.com/TechnologyEnhancedLearning/TELBlazor/blob/master/.releaserc.json)
	- you commits will look like this "docs(readme): added detail on commit rules"
1. Fetch, and checkout your new branch locally so you can add to the readme as you go
	- create commit "docs(readme): first commit"
	- see [commit rules](https://github.com/TechnologyEnhancedLearning/TELBlazor/blob/master/.commitlintrc.json)
	- *these rules also enable the versioning in cicd*
	- Bonus dont wait for the pipeline to fail your commit names and expose your secrets: you may want to add
		- gitguardian from confluence docs (follow it to the letter) [gitguardian global setup instructions](https://hee-tis.atlassian.net/wiki/spaces/TP/pages/3855253505/GitGuardian+Setup+-+Simplified+Version)
        - and add a pre-commit and push hook (you need both as you cannot lint what hasnt yet been commit) you add these to your templates if you want them for every repo, or just to this repo, or you can be lazy and add them into the gitguardian hook
		-	```
				#### --- Commitlint Logic (force local config) ---
				REPO_ROOT=$(git rev-parse --show-toplevel)
				CONFIG_PATH="$REPO_ROOT/.commitlintrc.json"
				echo "COMMIT_MSG_FILE is one msg behind it is only set to the commit your trying to commit after the commit succeeds so this script is one behind"
				echo "so would work best in prepush template"
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

#### Create local files
- Right click PackgeSettings.props.local and open with xml editor or any preffered way of opening it
- copy paste PackgeSettings.props.local.template into it
- Settings Detail
	- Recommended starting point
		- TELBlazorPackageSource -> create a local environmental variable system wide to where ever you want to make packages on your machine
			(go into windows edit environmental system variables, then look for environmental variables button, then add to system wide). This will be useful for 
			developing a consuming project and the package at the same time and making sure the changes will work.
		- NupkgOutputPath set this to the same variable as above
		- UseTELBlazorComponentsProjectReference set it true for faster development
		- TELBlazorPackageVersion set it to a number higher than the production value and increase it everytime you want to produce and use the package locally if not using the project reference
		  (this could be an environmental variable and set by the build number or some increasing number to allow automated increments across this project and a consuming project )
		- DisablePackageGeneration we publish the package on build if this isnt flagged. set it to true so can build the solution without making the package
		- E2ETracingEnabled set to true its for testing
		- HeadlessTesting set to true unless you want to see what the E2E tests are doing in a browser while they test
	- you can copy paste PackageSettings.props.local.template and then change them
#### Create More Environment Variables
- To use remote git hosted nuget packages you need a personal git token. This is just because git tracks the use of packages rather than it being anonymous
	- go onto your git profile
	- go to settings -> developer settings -> Personal access tokens -> Tokens classic
		- as a minimum select read:packages and you may wish to increase the expiration date.
			- copy the token it will disapear
	- Set system wide environment variables as before
		- GITHUB_USERNAME
		- GITHUB_PACKAGES_TOKEN
		- TELBlazorPackageSource
			- previously we set this to a local location but if you were to want to generally use the remote package this is the source 
			- https://nuget.pkg.github.com/TechnologyEnhancedLearning/index.json
			- for the test set this to https://nuget.pkg.github.com/TechnologyEnhancedLearning/index.json but you will probably want to point it back to a local folder afterwards
    - check credentials are working
		- open powershell somewhere you can put deletable content
			- ``` 
				# Create output folder if it doesn't exist
				New-Item -ItemType Directory -Path deleteme-test -ErrorAction SilentlyContinue

				# Build the auth string (username:token)
				$auth = "$($env:GITHUB_USERNAME):$($env:GITHUB_PACKAGES_TOKEN)"

				# Build base URL by removing trailing /index.json from the feed URL
				$baseUrl = $env:TELBlazorPackageSource -replace "/index\.json$", ""
				$baseUrl = $baseUrl.TrimEnd('/')

				# Download the package with curl using authentication
				curl.exe -u $auth `
				  -L "$baseUrl/download/TELBlazor.Components/1.0.0/TELBlazor.Components.1.0.0.nupkg" `
				  -o deleteme-test\TELBlazor.Components.1.0.0.nupkg

			```
	    - check there is a nupkg package. its an old one, so just delete it. 

#### Create appsettings
*Be aware that because WASM code is in the browser appsettings in the wasm client projects are not secret and sensitive data should not go in them*

- TELBlazor.Components.ShowCase.E2ETests.WasServerHost
	- create appsettings.Development.json
	- copy paste from appsettings.Development.json.template
	- the template is source controlled so wont have anything that needs to be secure
	- if you have preferences for logging this is where to configure it, if you want to add creating a text file for example add it here and then add it as dependency in the solution and a reference in the program.cs
	- logging can be used to get information during testing and test against it
- TELBlazor.Components.ShowCase.E2ETests.WasmServerHost.Client
	- follow the same process
	- client side appsetting are exposed through the wasm so dont put secure information in them
	- the client will have different logging options as it can only log to browser console, http to a logging api, storage
- TELBlazor.Components.ShowCase.ShowCase.WasmServerHost.Client
	- this is the project that become the gh-pages
	- the appsettings go into the wasm so no secrets in here
	- any of these projects could be used to view changes but if not looking at nojs this project could have different appsettings just for development as it isnt used to test against.
= TELBlazor.Components.UnitTests
		- as above

#### Install packages
- first set yourpackage.settings.props.local if you havent already via environment variables and hard coding to the following
		- TELBlazorPackageSource -> a local folder outside of the solution
		- NupkgOutputPath -> the same local folder outside of the solution
		- UseTELBlazorComponentsProjectReference -> true
		- TELBlazorPackageVersion -> 1.0.0 will do for now
		- DisablePackageGeneration -> true
		- E2ETracingEnabled -> true
		- HeadlessTesting -> true
- right click the solution and copy full path (we need admin rights so dont just open terminal)
- open powershell from windows as administrator
- cd and paste to your solution
- ```   
		# 1. Check environment variables and local props
		Write-Output "Have you set your environment variables and local props?"
        dotnet clean
		
		# 2. Restore NuGet packages (reads central package versions and props)
		Write-Output "Restore Nuget"
		dotnet restore

		# 3. Restore .NET CLI tools (Playwright, report manager for code coverage)
		Write-Output "Restore Tools"
		dotnet tool restore

		# 4. Install Node dependencies (gulp, playwright, frontend libs)
		Write-Output "Restore Node"
		npm install
		
		# 5. Build solution or run other commands as needed
		Write-Output "Build solution without build package, using project references instead of local package or remote package"
		dotnet build

```



#### Check Setup 

- Run tests in test runner
- run ps1
- check report generator
- use these setting to check credentials get remote package
- check make local package
- check works local package consumption





### How to set up this solution locally
*Be aware wasm wwwroot appsettings are public*
- TELBlazor\TELBlazor.Components.ShowCase.E2ETests\bin\Debug\net8.0\playwright.ps1 need to be run as admin
- local hook for commits, recommended for pre commit, and pre push as can only check the commit name value post commit


- open powershell admin
	- go to e2e project bin/Debug/net8
	- run the playwright script with "install"

## How to include package
1. Select a production version of the package [Package list for TELBlazor.Component on git](https://github.com/TechnologyEnhancedLearning/TELBlazor/pkgs/nuget/TELBlazor.Components)
1. Set up css references and dependency injection using lean host examples WasmServerHost, WasmServerHost.Client and WasmStaticClient
from the repo and ShowCase project for how to include the package. 
1. You will need a copy of nhsuk.css and a reference <link href="css/nhsuk.css" rel="stylesheet" /> see gulp in the previously mentioned projects


# Solution and Pipeline

## Features of solution
- run-tests-and-report-with-env-values.ps1 runs test similar to pipeline
	- create html coverage report and threshold check (recommendation: bookmark local html file in browser to easily view)
	
## Features of CICD
- There is a readme in the CICD
- A DevShowCase sight is created using a DevPackage and the same in production
	- The dev pipeline also publishes a coverage report

## Local Files and Development Settings


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
	- Wasm global hosted
 - TELBlazor.Components.ShowCase.E2ETests.WasmServerHost.Client 
	- Wasm global hosted
 - TELBlazor.Components.ShowCase.WasmStaticClient
	- Wasm global standalone 
	
## Configuration

### Logging
- For more detailed dependency injected logging see MVCBlazor project	
	
## Notes

### Stuff you don't need to know (but may be useful for a specific issue on searching the readme)
- It is not render auto per components because the intention is to be used in MVC views.
- Xunit is used with Bunit and Nunit with playwright, either could be 
changed so that they are using the same and this could be done in future 
as the libraries improve but currently each is being used with the 
recommend tool it is designed for though both support the others tool.
- not using data-testid="TELButton" because we should use aria selectors. We may change this later.
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