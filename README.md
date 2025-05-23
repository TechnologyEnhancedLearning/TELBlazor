
# TELBlazor
TEL Blazor Component Library Package

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

[View Packaged Dev Showcase Code](https://technologyenhancedlearning/TELBlazor-DevShowCase/tree/gh-pages/)

[View Packaged Showcase Code](https://github.com/TechnologyEnhancedLearning/TELBlazor/tree/gh-pages/)

[Code Report Page](https://technologyenhancedlearning.github.io/TELBlazor-CodeReport/)

[NHSE TEL Frontend](https://github.com/TechnologyEnhancedLearning/nhse-tel-frontend)

# Set Up

## How to set up this solution locally
*Be aware wasm wwwroot appsettings are public*
- TELBlazor\TELBlazor.Components.ShowCase.E2ETests\bin\Debug\net8.0\playwright.ps1 need to be run as admin
- local hook for commits, recommended for pre commit, and pre push as can only check the commit name value post commit

`
# --- Commitlint Logic (force local config) ---
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
`
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