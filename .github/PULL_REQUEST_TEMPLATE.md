# PR Template

### JIRA/RoadMap
_Provide your ticket number
[TD-####](https://hee-tis.atlassian.net/browse/TD-####)
[Git Ticket Board](https://github.com/orgs/TechnologyEnhancedLearning/projects/9)[Ticket Board](https://github.com/orgs/TechnologyEnhancedLearning/projects/9)
[Git Ticket](https://github.com/TechnologyEnhancedLearning/TELBlazor/issues/42)

### Description
_Describe what has changed and how that will affect the app. If relevant, add links to any sources/documentation you used. Highlight anything unusual and give people context around particular decisions._

### Resulting Dev package
_Your dev generated package name and reference
[TELBlazor published packages](https://github.com/TechnologyEnhancedLearning/TELBlazor/pkgs/nuget/TELBlazor.Components)
Dev packages are follow by branch name -branch-name and are generated
Will this code change result in a production package version increase 

### Screenshots
_Paste screenshots for all views created or changed: mobile, tablet and desktop, wave analyser showing no errors._

### Linked package consumer tasks
[TD-####](https://hee-tis.atlassian.net/browse/TD-####)
[ ] Consumers also need to update nhsuk css dependency to : __put_required_nhsuk_semantic_version_here__

### Logging
_Provide description of any component scoped logging or specific level logging to check

### Size Optimisation
_Provide wasm size comparison between prod and dev showcases, does it exceed 8Mb
- Network tab in Chrome
- Disable cache
- reload
- filter wasm dll js dotnet.wasm, blazor.webassembly.js, TELBlazor.dll
- if using compression check .br or .gz (if Content-Encoding: br or gz is in headers)
- Sum up

| Showcase | Total WASM Size (Compressed, MB) | Notes |
|----------|----------------------------------|-------|
| [Dev](https://technologyenhancedlearning.github.io/TELBlazor-DevShowCase/)      | X.X MB                          |       |
| [Prod](https://technologyenhancedlearning.github.io/TELBlazor/)     | Y.Y MB                          |       |
| Difference |   |  |

-----
### Developer checks
(Leave tasks unticked if they haven't been appropriate for your ticket.)

I have:
- [ ] Provided showcase example of component if applicable
- [ ] Added appropriate logging and scopped logging reporting in appsettings for component if applicable
- [ ] Updated readme documentation
- [ ] Updated showcase documentation for component
- [ ] I have locally run tests against a local package (not just using project reference)
- [ ] Used a browser set to No Js before using it to locally run and test changes (recommend brave as second browser)
- [ ] Written Unit tests with accesibility syntax
- [ ] Written E2E tests with accesibility syntax and accessibility test
- [ ] Tested components with [Wave Chrome plugin](https://chrome.google.com/webstore/detail/wave-evaluation-tool/jbbplnpkjmmeebjpijfedlgcdilocofh/related). Addressed any valid accessibility issues and documented any invalid errors
- [ ] [Check code coverage](https://technologyenhancedlearning.github.io/TELBlazor-CodeReport/) or locally with local report generation
- [ ] Updated my Jira ticket with testing notes, including information about other parts of the system that were touched as part of the PR and need  to be tested to ensure nothing is broken
- [ ] Tested in [Dev Showcase](https://technologyenhancedlearning.github.io/TELBlazor-DevShowCase/) (including logging by using log level switcher)
- [ ] Scanned over my pull request and commented with any useful explanations/questions to reviewers
- [ ] Scanned over cicd warnings relating to the component or area of code I have worked on (give the general ones a look too but antyhing in OptionalImplementations/Test can be ignored)

---
### Peer Reviewers and Assignee checks before Approval
- [ ] Feedback has been provided
- [ ] Project has been run locally (you can provide pr feedback via vs if desired)
- [ ] Locally checked in browser set to No Js from before load (recommend Brave with no js settings)
- [ ] [Dev Showcase](https://technologyenhancedlearning.github.io/TELBlazor-DevShowCase/) was checked and it was checked the package number matched the PR
- [ ] In Dev Showcase checked against different logging levels if applicable (use log level switcher to change level)
- [ ] All conversations have been responded to (emoji will do) and marked resolved
- [ ] Out of scope code observations have been recorded to inform future tasks
- [ ] Common questions / Architectural explanations decisions from PR documented
- [ ] [Check code coverage](https://technologyenhancedlearning.github.io/TELBlazor-CodeReport/)
- [ ] Should E2E or Unit test have been added
- [ ] If the published dev package is linked and used in tandom with a package consumer task is it working locally (Not a hard requirement but useful in case changes required)
- [ ] Checked component readme in Showcase

---
### Post PR Intentions and Actions
- [ ] On merge will someone check [Prod Showcase](https://technologyenhancedlearning.github.io/TELBlazor/)
- [ ] Tick yes if consuming projects need a version bump and/or code changes to take advantage of new components etc
- [ ] If there is a linked consuming project task has the task assignee, or task been updated to know the package is available as a dev/prod version and been provided the version number.