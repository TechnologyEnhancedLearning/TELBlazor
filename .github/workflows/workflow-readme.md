# Scripts

## Dev
Dev Check Test Package TELBlazor.Components Trigger TELBlazor-DevShowCase Deployment

- Checks should all run so if multiple fails can be resolved in one commit
- Artifact and workflow trigger for GH-Page deployment TELBlazor-DevShowCase
- branch name check logic should be kept upto date with releaserc.json and commitlintrc.json
- commitlint would be better as a prehook but global gitguardian hook seems to interfere
- assume git guardian global and prehook

## Pull_Request


## Release

# Notes
- doesnt run easily with nektos act due to git ref checks and calling other workflows