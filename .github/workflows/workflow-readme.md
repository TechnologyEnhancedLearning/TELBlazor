
# Contributions
- conventions should be
	- jobs kebab case no caps
	- step names sentances
	- step ids snake case
	- envs all cap snake


# Scripts

## Dev
Dev Check Test Package TELBlazor.Components Trigger TELBlazor-DevShowCase Deployment

- Run checks from reeuseable ci checks workflow
- versions repo and package
- Artifact and workflow trigger for GH-Page deployment TELBlazor-DevShowCase


## Pull_Request
- just runs on all pull requests the Reuseable Ci Checks
- The pull request, and the branch rules to do the same checks currently. The advantage of the branch rules are that
they stay in the pull request ui. they are also only targetted on master
 
 
## Reuseable Ci Checks
- Checks should all run so if multiple fails can be resolved in one commit but still trigger a stopping error if any fail at the end of the workflow.

 
## Release
- if there is a version change it updates
	- repo tag
	- packages with new package and version
	- TELBlazor-ShowCase site
	
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

