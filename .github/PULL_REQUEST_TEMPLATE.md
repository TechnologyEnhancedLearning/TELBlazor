# PR Template

### JIRA/RoadMap
_Provide your ticket number
[TD-####](https://hee-tis.atlassian.net/browse/TD-####)
[Git Ticket Board](https://github.com/orgs/TechnologyEnhancedLearning/projects/9)
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
- [ ] Consumers also need to update nhsuk css dependency to : __put_required_nhsuk_semantic_version_here__

### Logging
_Provide description of any component scoped logging or specific level logging to check

### Performance comparison

- [ ] Check performance and tick the box. Or complete the table or just a note for the relevant row.



Use two browsers both **Incognito** mode and **disable cache** in the Network tab to get a cold load comparison for 
[Dev showcase](https://technologyenhancedlearning.github.io/TELBlazor-DevShowCase/) for your package version.
[Prod showcase](https://technologyenhancedlearning.github.io/TELBlazor/) for a comparison of the change.


**Open Incognito Browsers**
1. Open the Developer Tools (F12 or Ctrl+Shift+I)
1. Go to networking
1. Clear log
1. record
1. Disable cache
1. Then go to lighthouse tab

**Lighthouse**

1. Go to the **Lighthouse** tab.
1. Deselect the "SEO" category.
1. Select "Desktop" for the device.
1. Choose "Navigation (Analyze page load)".
1. Click "Analyze page load" and wait for the results.

Record in the table, or compare for change:

* **First Contentful Paint**: The time it takes for the first text or image to appear. A lower value is better.
* **Speed Index**: How quickly content is visually displayed during page load. A lower value is better.
* **Largest Contentful Paint**: The time it takes for the largest content element to be rendered. A lower value is better.
* **Total Blocking Time**: The total amount of time between First Contentful Paint and Time to Interactive where the main thread was blocked for long enough to prevent input responsiveness. A lower value is better.

**Network**

1. Go to the **Network** tab.
1. Copy and paste the bottom line of the log to get the following metrics:

Record in the table, or compare for change:
* **Requests**: The total number of requests made.
* **Transferred**: The compressed size of all transferred resources.
* **Resources**: The total uncompressed size of all resources.
* **Finish**: The time from the request initiation to the completion of the last response.

You can also use the `.wasm` filter in the Network tab to inspect the sizes of individual WebAssembly files.

**Wave Test**
1. Right click page with WAVE plugin installed and run wave
1. Check component is WCAG AAA navigate to component page

Please fill in the table below with the values from both the Dev and Prod environments.
*or just check the values if its quicker and make sure to highlight anything concerning*
| Measure | [Dev Showcase](https://technologyenhancedlearning.github.io/TELBlazor-DevShowCase/) | [Prod](https://technologyenhancedlearning.github.io/TELBlazor/) | Notes |
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
| **WCAG AAA** | | | |


-----
### Developer checks
(Leave tasks unticked if they haven't been appropriate for your ticket.)

I have:
- [ ] Provided showcase example of component
- [ ] Added appropriate logging and scoped logging reporting in appsettings for component if applicable
- [ ] Updated readme documentation
- [ ] Updated showcase documentation for component
- [ ] Used a browser set to No Js before using it to locally run and test changes (recommend brave as second browser)
- [ ] Written Unit tests with accesibility syntax
- [ ] Written E2E tests with accesibility syntax and accessibility test
- [ ] Tested components with [Wave Chrome plugin](https://chrome.google.com/webstore/detail/wave-evaluation-tool/jbbplnpkjmmeebjpijfedlgcdilocofh/related). Addressed any valid accessibility issues and documented any invalid errors
- [ ] Check code coverage [Dev pipeline report page](https://technologyenhancedlearning.github.io/TELBlazor-CodeReport/) or locally with local report generation. (Focus on new area)
- [ ] Updated my Jira ticket with testing notes, including information about other parts of the system that were touched as part of the PR and need  to be tested to ensure nothing is broken
- [ ] Tested in [Dev Showcase](https://technologyenhancedlearning.github.io/TELBlazor-DevShowCase/) (including logging by using log level switcher)
- [ ] Scanned over my pull request and commented with any useful explanations/questions to reviewers
- [ ] Scanned over cicd warnings relating to the component or area of code I have worked on (give the general ones a look too but antyhing in OptionalImplementations/Test can be ignored)
- [ ] Auditted new NuGet packages prefering lightweight ones (e.g., System.Text.Json) and ensuring third-party components support trimming.
- [ ] Looked at build messages specifically for new code.

---
### Peer Reviewers and Assignee checks before Approval
- [ ] Feedback has been provided
- [ ] Project has been run locally
- [ ] Locally checked in browser set to No Js from before load (recommend Brave with no js settings)
- [ ] [Dev Showcase](https://technologyenhancedlearning.github.io/TELBlazor-DevShowCase/) was checked and it was checked the package number matched the PR
- [ ] In Dev Showcase checked against different logging levels if applicable (use log level switcher to change level)
- [ ] All conversations have been responded to (emoji will do) and marked resolved
- [ ] Out of scope code observations have been recorded to inform future tasks
- [ ] Common questions / Architectural explanations decisions from PR documented
- [ ] Checked [code coverage](https://technologyenhancedlearning.github.io/TELBlazor-CodeReport/)
- [ ] Checked not missing E2E or Unit tests
- [ ] Linked task for the consumption of the new component has been given a link to the dev package to help development.
- [ ] Checked component readme in Showcase

---
### Post PR Intentions and Actions
- Check [Prod Showcase](https://technologyenhancedlearning.github.io/TELBlazor/)
- If prod bumps the version share that the consuming projects should update to the nevew version.