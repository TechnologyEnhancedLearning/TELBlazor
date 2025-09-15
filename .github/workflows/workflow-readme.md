
# Contributions
- conventions should be
	- jobs kebab case no caps
	- step names sentances
	- step ids snake case
	- envs all cap snake


# Scripts

## Dev

### Overview
Dev Check Test Package TELBlazor.Components Trigger TELBlazor-DevShowCase Deployment

- Run checks from reeuseable ci checks workflow
- versions repo and package
- Artifact and workflow trigger for GH-Page deployment TELBlazor-DevShowCase

### Detail
- Uses Nugetkey token which is an organisational token
- the project at the solution level has a CICDPackageLocation folder the route to which is defined by ```LOCAL_PACKAGES_PATH : ${{ github.workspace }}/CICDPackageLocation``` 
	- This is the path we build the package to before giving it to the repo git package area
- ```dev-call-reusable-ci-checks-workflow``` just build checks and tests
- ```generate-dev-semantic-version```
	- ```    outputs:
		  dev-package-version: ${{ steps.set_dev_semantic_version.outputs.dev-semantic-version }}```
		- the id is used to reference what step is setting the value of dev-package-version
			- it is dev package version because we version the dev package versions seperately
	- npm installs sematic-release
	- we run npx sematic-release for the version
		- there can be no version if no change so then we just use the repo tag
		- we add the date too
		- the package version for dev does not need to be the right order it just needs to have one for easy development its the release version that matters
- next set_dev_semantic_version just adds a timestamp to the string
- build-telblazor-dev-package-and-publish
	- we use nuget.config.cicd as the nuget template which takes our environmental values via text replace
	- we clean lock files because there is about to be a version change
	- we build
		- build creates a partial which has the version in it which is useful for the ShowCase
		- The project has a flag that allows building to publish the Package
		- it uses the nuget output path to the CICD folder at solution level
	- We publish the Package
- trigger-gh-pages-telblazor-devshowcase-workflow
	- makes the showcase using the package

## Pull_Request
- just runs on all pull requests the Reuseable Ci Checks
- The pull request, and the branch rules to do the same checks currently. The advantage of the branch rules are that
they stay in the pull request ui. they are also only targetted on master
 
 
## Reuseable Ci Checks
- Checks should all run so if multiple fails can be resolved in one commit but still trigger a stopping error if any fail at the end of the workflow.

 
## Release
### Overview
- if there is a version change it updates
	- repo tag
	- packages with new package and version
	- TELBlazor-ShowCase site
### Detail

	
## dependabot
- npm and node
- seperate script to auto merge

## automerge-passing-minor-patch-dependabot-prs
- 

# Git setup

## Pull requests
- Branch checks for master dont work, they don't run the workflow dispatch.
If we want these checks one solution is every check in its own yml (they dont directly use Reuseable Ci Checks instead they use them via the pull_request yml)
The individual steps also automatically pass so can see if any error at the end with the check to check all their outputs

# Notes
- doesnt run easily with nektos act due to git ref checks and calling other workflows
- for tests use the run-tests-and-report-with-env-values.ps1 file
- dependabot duplicates tokens using dependabot secrets including write so can run checks
- autoverging is being tried for major and minor
- branch checks must pass for merge on automated_version
- checks required but overrideable for all workflows
- dependabot secret names to match repos ones where need to share
- dependabot not need to build package later brnch does

## Dependabot Pipeline (AI generatated diag)

```mermaid
flowchart TD
    %% Dependabot PR to initial branch
    A[Dependabot PR] --> B[Automatic_version_update_dependabot]

    %% Checks on the dependabot branch
    B --> C[Run Checks]
    C --> C1[Commit name check ❌ skipped]
    C --> C2[Branch name check ❌ skipped]
    C --> C3[Build as release]
    C --> C4[Unit tests]
    C --> C5[E2E tests]

    %% Weekly merge to staging
    B -->|Weekly merge via collected-dependabot-to-staging.yml| D[Automatic_collected_dependabot_staging]

    %% Staging checks and dev build
    D --> E[Run Checks & Dev Build]
    E --> E1[Checks again]
    E --> E2[Build dev package]
    E --> E3[Showcase dev page]

    %% Weekly merge to master
    D -->|Weekly merge via collected-dependabot-staging-to-master.yml| F[Master]
```

## Versioning
Via semantic release and recorded as a generate c# file used by a blazor component

## Alternative Approaches

- dont use pull-request-target for security reasons if can avoid it and if do use ifs to control it based on what branch and who is calling the workflow
- can use secrets inherits might have been better for reuseable checks which because triggered by other workflows can directly access repo secrets instead need them passing
