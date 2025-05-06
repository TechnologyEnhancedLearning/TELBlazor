# Scripts

## Dev
Dev Check Test Package TELBlazor.Components Trigger TELBlazor-DevShowCase Deployment

- Run checks
- Artifact and workflow trigger for GH-Page deployment TELBlazor-DevShowCase
- branch name check logic should be kept upto date with releaserc.json and commitlintrc.json
- commitlint would be better as a prehook but global gitguardian hook seems to interfere
- assume git guardian global and prehook

## Pull_Request
- The pull request, and the branch rules to do the same checks currently. The advantage of the branch rules are that
they stay in the pull request ui. they are also only targetted on master
 
 
## Reuseable Ci Checks
- Checks should all run so if multiple fails can be resolved in one commit but still trigger a stopping error if any fail at the end of the workflow.
 
## Release

# Notes
- doesnt run easily with nektos act due to git ref checks and calling other workflows
