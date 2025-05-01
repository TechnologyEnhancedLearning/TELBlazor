
# todo dont forget
- remove the non condition project reference from shared to teblazor.components

# TELBlazor
TEL Blazor Component Library Package

# Purpose

Progressive components, that use the server prerendering in Global Wasm Blazor to ensure that if the user has no JS they will get html. And that html can be created to have working post actions.
The render cycle will hydrate the prerender and the post actions will be overrided by services injected in the components.
It is client side so the users browser will do the work. 




# Architecture

## Project Structures used
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


## Packages on setup and purpose

TODO QQQQ Check which are needed some may now longer be useful
include reason for each being installed

### MVCBlazor prototype project packages

PS C:\dev\reposC\MVCBlazor> dotnet list package
Project 'LH.MVCBlazor.Server' has the following package references
   [net8.0]:
   Top-level Package                                         Requested   Resolved
   > Markdig                                                 0.39.1      0.39.1
   > Microsoft.AspNetCore.Components.WebAssembly.Server      8.0.8       8.0.8
   > Microsoft.Extensions.Http                               8.0.1       8.0.1
   > Serilog                                                 4.2.0       4.2.0
   > Serilog.AspNetCore                                      8.0.0       8.0.0
   > Serilog.Extensions.Logging                              8.0.0       8.0.0
   > Serilog.Formatting.Compact                              3.0.0       3.0.0
   > Serilog.Settings.Configuration                          8.0.0       8.0.0
   > Serilog.Sinks.Console                                   6.0.0       6.0.0
   > Serilog.Sinks.Debug                                     3.0.0       3.0.0
   > Serilog.Sinks.File                                      6.0.0       6.0.0
   > Serilog.Sinks.Http                                      8.0.0       8.0.0

Project 'Package.LH.BlazorComponents' has the following package references
   [net8.0]:
   Top-level Package                          Requested   Resolved
   > Microsoft.AspNetCore.Components          8.0.10      8.0.10
   > Microsoft.AspNetCore.Components.Web      8.0.8       8.0.8

Project 'Package.Shared.BlazorComponents' has the following package references
   [net8.0]:
   Top-level Package                            Requested   Resolved
   > Microsoft.AspNetCore.Components            8.0.8       8.0.8
   > Microsoft.AspNetCore.Components.Forms      8.0.8       8.0.8
   > Microsoft.AspNetCore.Components.Web        8.0.8       8.0.8
   > Microsoft.AspNetCore.Mvc.Core              2.2.5       2.2.5
   > Microsoft.Extensions.Logging               8.0.0       8.0.0

Project 'Package.LH.Services' has the following package references
   [net8.0]:
   Top-level Package                                            Requested   Resolved
   > Microsoft.Extensions.DependencyInjection.Abstractions      8.0.1       8.0.1

Project 'Package.Shared.Services' has the following package references
   [net8.0]:
   Top-level Package                                            Requested   Resolved
   > Blazored.LocalStorage                                      4.5.0       4.5.0
   > Microsoft.Extensions.DependencyInjection.Abstractions      8.0.1       8.0.1
   > Microsoft.Extensions.Http                                  8.0.0       8.0.0
   > Serilog                                                    4.2.0       4.2.0

Project 'Package.Shared.Entities' has the following package references
   [net8.0]: No packages were found for this framework.
Project 'Package.LH.Entities' has the following package references
   [net8.0]: No packages were found for this framework.
Project 'LH.Blazor.Client' has the following package references
   [net8.0]:
   Top-level Package                                               Requested    Resolved
   > Blazored.LocalStorage                                         4.5.0        4.5.0
   > Microsoft.AspNetCore.Components.WebAssembly                   8.0.8        8.0.8
   > Microsoft.AspNetCore.Components.WebAssembly.DevServer         8.0.8        8.0.8
   > Microsoft.Extensions.Http                                     8.0.1        8.0.1
   > Microsoft.NET.ILLink.Tasks                              (A)   [8.0.15, )   8.0.15
   > Microsoft.NET.Sdk.WebAssembly.Pack                      (A)   [9.0.4, )    9.0.4
   > Serilog                                                       4.2.0        4.2.0
   > Serilog.Extensions.Logging                                    8.0.0        8.0.0
   > Serilog.Formatting.Compact                                    3.0.0        3.0.0
   > Serilog.Settings.Configuration                                8.0.0        8.0.0
   > Serilog.Sinks.BrowserConsole                                  8.0.0        8.0.0
   > Serilog.Sinks.Http                                            8.0.0        8.0.0

Project 'LH.DB.API' has the following package references
   [net8.0]:
   Top-level Package                 Requested   Resolved
   > Serilog                         4.2.0       4.2.0
   > Serilog.AspNetCore              8.0.0       8.0.0
   > Serilog.Formatting.Compact      3.0.0       3.0.0
   > Serilog.Sinks.Console           6.0.0       6.0.0
   > Serilog.Sinks.Debug             3.0.0       3.0.0
   > Serilog.Sinks.File              6.0.0       6.0.0
   > Swashbuckle.AspNetCore          6.4.0       6.4.0

Project 'Package.Shared.BlazorComponents.UnitTests' has the following package references
   [net8.0]:
   Top-level Package                Requested   Resolved
   > AutoFixture                    4.18.1      4.18.1
   > AutoFixture.AutoMoq            4.18.1      4.18.1
   > bunit                          1.34.0      1.34.0
   > coverlet.collector             6.0.0       6.0.0
   > FluentAssertions               7.0.0       7.0.0
   > Microsoft.NET.Test.Sdk         17.11.1     17.11.1
   > xunit                          2.9.0       2.9.0
   > xunit.runner.visualstudio      2.5.4       2.5.4

Project 'Package.LH.BlazorComponents.UnitTests' has the following package references
   [net8.0]:
   Top-level Package                Requested   Resolved
   > AutoFixture                    4.18.1      4.18.1
   > AutoFixture.AutoMoq            4.18.1      4.18.1
   > bunit                          1.34.0      1.34.0
   > coverlet.collector             6.0.0       6.0.0
   > Microsoft.NET.Test.Sdk         17.11.1     17.11.1
   > xunit                          2.9.0       2.9.0
   > xunit.runner.visualstudio      2.5.4       2.5.4

Project 'Test.Components' has the following package references
   [net8.0]:
   Top-level Package                            Requested   Resolved
   > AutoFixture                                4.18.1      4.18.1
   > AutoFixture.AutoMoq                        4.18.1      4.18.1
   > FluentAssertions                           8.0.0       8.0.0
   > Microsoft.AspNetCore.Components.Forms      8.0.8       8.0.8
   > Microsoft.AspNetCore.Components.Web        8.0.8       8.0.8
   > Microsoft.AspNetCore.Mvc.Core              2.3.0       2.3.0

Project 'Test.BUnit.UnitTests' has the following package references
   [net8.0]:
   Top-level Package                        Requested   Resolved
   > bunit                                  1.34.0      1.34.0
   > coverlet.collector                     6.0.0       6.0.0
   > FluentAssertions                       8.0.1       8.0.1
   > Microsoft.NET.Test.Sdk                 17.11.1     17.11.1
   > Serilog                                4.2.0       4.2.0
   > Serilog.Expressions                    5.0.0       5.0.0
   > Serilog.Extensions.Logging             8.0.0       8.0.0
   > Serilog.Formatting.Compact             3.0.0       3.0.0
   > Serilog.Settings.Configuration         8.0.0       8.0.0
   > Serilog.Sinks.InMemory                 0.14.0      0.14.0
   > Serilog.Sinks.InMemory.Assertions      0.14.0      0.14.0
   > Serilog.Sinks.XUnit                    2.0.4       2.0.4
   > xunit                                  2.9.0       2.9.0
   > xunit.runner.visualstudio              2.5.4       2.5.4

Project 'Test.BrowserBased.Host' has the following package references
   [net8.0]:
   Top-level Package                                         Requested   Resolved
   > Microsoft.AspNetCore.Components.WebAssembly.Server      8.0.13      8.0.13

Project 'Test.BrowserBased.Host.Client' has the following package references
   [net8.0]:
   Top-level Package                                     Requested    Resolved
   > Microsoft.AspNetCore.Components.WebAssembly         8.0.13       8.0.13
   > Microsoft.NET.ILLink.Tasks                    (A)   [8.0.15, )   8.0.15
   > Microsoft.NET.Sdk.WebAssembly.Pack            (A)   [9.0.4, )    9.0.4

Project 'Test.BrowserBased.UnitE2ETests' has the following package references
   [net8.0]:
   Top-level Package                       Requested   Resolved
   > AutoFixture                           4.18.1      4.18.1
   > coverlet.collector                    6.0.0       6.0.0
   > Deque.AxeCore.Playwright              4.10.1      4.10.1
   > FluentAssertions                      8.0.0       8.0.0
   > Microsoft.AspNetCore.Mvc.Testing      8.0.0       8.0.0
   > Microsoft.NET.Test.Sdk                17.8.0      17.8.0
   > Microsoft.Playwright                  1.50.0      1.50.0
   > Microsoft.Playwright.MSTest           1.50.0      1.50.0
   > Microsoft.Playwright.Xunit            1.50.0      1.50.0
   > Verify.Playwright                     3.0.0       3.0.0
   > xunit                                 2.5.3       2.5.3
   > xunit.runner.visualstudio             2.5.3       2.5.3



# Stuff you don't need to know
- It is not render auto per components because the intention is to be used in MVC views.
- Xunit is used with Bunit and Nunit with playwright, either could be 
changed so that they are using the same and this could be done in future 
as the libraries improve but currently each is being used with the 
recommend tool it is designed for though both support the others tool.