
# TELBlazor
TEL Blazor Component Library Package

# Purpose

Progressive components, that use the server prerendering in Global Wasm Blazor to ensure that if the user has no JS they will get html. And that html can be created to have working post actions.
The render cycle will hydrate the prerender and the post actions will be overrided by services injected in the components.
It is client side so the users browser will do the work.
  
   
# Links

[last published package](https://github.com/orgs/TechnologyEnhancedLearning/packages?tab=packages&q=TELBlazor)

[TELBlazor](https://github.com/TechnologyEnhancedLearning/TELBlazor)

[TELBlazor Production Showcase](https://technologyenhancedlearning.github.io/TELBlazor/)

[TELBlazor Dev Showcase](https://technologyenhancedlearning.github.io/TELBlazor-DevShowCase/)

[View Packaged Dev Showcase Code](https://technologyenhancedlearning/TELBlazor-DevShowCase/tree/gh-pages/)

[View Packaged Showcase Code](https://github.com/TechnologyEnhancedLearning/TELBlazor/tree/gh-pages/)

[Code Report Page](https://technologyenhancedlearning.github.io/TELBlazor-CodeReport/)

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