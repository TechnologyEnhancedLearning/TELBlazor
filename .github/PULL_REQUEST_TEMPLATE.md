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

### Performance comparison

Complete the below comparison table to check for any concerning changes in performance.

Use **Incognito** mode and **disable cache** in the Network tab to get a cold load comparison.

**Lighthouse**

1. Open the Developer Tools (F12 or Ctrl+Shift+I).
2. Go to the **Lighthouse** tab.
3. Deselect the "SEO" category.
4. Select "Desktop" for the device.
5. Choose "Navigation (Analyze page load)".
6. Click "Analyze page load" and wait for the results.

Here's an example of what to look for and compare:

* **First Contentful Paint**: The time it takes for the first text or image to appear. A lower value is better.
* **Speed Index**: How quickly content is visually displayed during page load. A lower value is better.
* **Largest Contentful Paint**: The time it takes for the largest content element to be rendered. A lower value is better.
* **Total Blocking Time**: The total amount of time between First Contentful Paint and Time to Interactive where the main thread was blocked for long enough to prevent input responsiveness. A lower value is better.

**Network**

1. Go to the **Network** tab.
2. Ensure **"Disable cache"** is checked.
3. **Clear** the log.
4. **Preserve log** can be checked to maintain a history of requests.
5. Perform a hard reload (Ctrl+Shift+R or Cmd+Shift+R).
6. Copy and paste the bottom line of the log to get the following metrics:

* **Requests**: The total number of requests made.
* **Transferred**: The compressed size of all transferred resources.
* **Resources**: The total uncompressed size of all resources.
* **Finish**: The time from the request initiation to the completion of the last response.

You can also use the `.wasm` filter in the Network tab to inspect the sizes of individual WebAssembly files.

---

### Comparison Table

Please fill in the table below with the values from both the Dev and Prod environments.
*or just check the values if its quicker and make sure to highlight anything concerning*
| Measure | [Dev Showcase](https://technologyenhancedlearning.github.io/TELBlazor-DevShowCase/) | [Prod](https://technologyenhancedlearning.github.io/TELBlazor/) | Notes (E.g. Significant change) |
| :--- | :--- | :--- | :--- |
| **Lighthouse Performance Score** | | | |
| **Lighthouse Accessibility Score** | | | |
| **Lighthouse Best Practices Score** | | | |
| **First Contentful Paint** | | | |
| **Speed Index** | | | |
| **Total Blocking Time** | | | |
| **Largest Contentful Paint** | | | |
| **Transferred (Cold Load Payload)** | | | |
| **Resources (Full App Weight)** | | | |
| **Requests** | | | |
| **Finish Time** | | | |


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
- [ ] Maybe? Audit NuGet packages; use lightweight ones (e.g., System.Text.Json); ensure third-party components support trimming.
- [ ] Scanned in visual studio build info messages about improving code for new code
- [ ] 
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