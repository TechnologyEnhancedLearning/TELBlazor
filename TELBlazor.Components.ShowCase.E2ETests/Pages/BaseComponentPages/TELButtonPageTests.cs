using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Deque.AxeCore.Commons;
using Deque.AxeCore.Playwright;
using TELBlazor.Components.ShowCase.E2ETests.Helpers;
using FluentAssertions;
using System.Text.RegularExpressions;

namespace TELBlazor.Components.ShowCase.E2ETests.Pages.BaseComponentPages
{

    public class TELButtonPageTests : BlazorPageTest<Program>
    {
        private readonly bool _tracingEnabled;
        public TELButtonPageTests()
        {
            _tracingEnabled = (bool.TryParse(Environment.GetEnvironmentVariable("E2ETracingEnabled"), out var result) && result);
        }

        // Axe needs js
        [Theory]
        [InlineData("chromium", true, ViewportHelper.ViewportType.Desktop)]
        //[InlineData("chromium", false, ViewportHelper.ViewportType.Desktop)]
        [InlineData("chromium", true, ViewportHelper.ViewportType.Mobile)]
        //[InlineData("chromium", false, ViewportHelper.ViewportType.Mobile)]

        [InlineData("firefox", true, ViewportHelper.ViewportType.Desktop)]
        //[InlineData("firefox", false, ViewportHelper.ViewportType.Desktop)]
        [InlineData("firefox", true, ViewportHelper.ViewportType.Mobile)]
        //[InlineData("firefox", false, ViewportHelper.ViewportType.Mobile)]

        [InlineData("webkit", true, ViewportHelper.ViewportType.Desktop)]
        //[InlineData("webkit", false, ViewportHelper.ViewportType.Desktop)]
        [InlineData("webkit", true, ViewportHelper.ViewportType.Mobile)]
        //[InlineData("webkit", false, ViewportHelper.ViewportType.Mobile)]

        public async Task TELBlazorButtonMeetsAxeAccesibilityStandards(string browserType, bool jsEnabled, ViewportHelper.ViewportType viewport)
        {

            using IPlaywright playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            IBrowserContext browserContext = await BrowserHelper.CreateBrowserContextAsync(playwright, browserType, jsEnabled, viewport, BaseUrl);
            //Debug option
            if (_tracingEnabled)
            {
                await browserContext.Tracing.StartAsync(new()
                {
                    Screenshots = true,
                    Snapshots = true,
                    Sources = true,
                });
            }


            IPage page = await browserContext.NewPageAsync();

            await page.GotoOnceNetworkIsIdleAsync("TELButtonPage");
            ILocator button = page.GetByRole(AriaRole.Button, new() { Name = "Click Counter" });

            AxeResult axeResults = await button.RunAxe();

            axeResults.Violations.Should().BeNullOrEmpty();

            if (_tracingEnabled)
            {
                string methodName = "TELBlazorButtonMeetsAxeAccesibilityStandards";
                string timestamp = DateTime.UtcNow.ToString("yy_MM_dd_HH_mm_ss", CultureInfo.InvariantCulture);
                string arguments = $"{browserType}_{$"jsEnabled_{jsEnabled.ToString()}"}_{viewport.ToString()}";
                string path = $"../../../Reports/{methodName}_{arguments}_{timestamp}.zip";
                await browserContext.Tracing.StopAsync(new()
                {
                    Path = path,
                });
            }

            // Clean up resources by closing the page and browser context
            await page.CloseAsync();
            await browserContext.CloseAsync();
        }


        [Theory]
        [InlineData("chromium", true, ViewportHelper.ViewportType.Desktop)]
        [InlineData("chromium", false, ViewportHelper.ViewportType.Desktop)]
        [InlineData("chromium", true, ViewportHelper.ViewportType.Mobile)]
        [InlineData("chromium", false, ViewportHelper.ViewportType.Mobile)]

        [InlineData("firefox", true, ViewportHelper.ViewportType.Desktop)]
        [InlineData("firefox", false, ViewportHelper.ViewportType.Desktop)]
        [InlineData("firefox", true, ViewportHelper.ViewportType.Mobile)]
        [InlineData("firefox", false, ViewportHelper.ViewportType.Mobile)]

        [InlineData("webkit", true, ViewportHelper.ViewportType.Desktop)]
        [InlineData("webkit", false, ViewportHelper.ViewportType.Desktop)]
        [InlineData("webkit", true, ViewportHelper.ViewportType.Mobile)]
        [InlineData("webkit", false, ViewportHelper.ViewportType.Mobile)]
        public async Task TELButtonPage_InteractivityIsCorrectlySimulated(string browserType, bool jsEnabled, ViewportHelper.ViewportType viewport)
        {

            //Handling this per test and running them all on the browser is quick for writing tests slow for running test
            //alternatively we could have a browser per test file
            using IPlaywright playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            IBrowserContext browserContext = await BrowserHelper.CreateBrowserContextAsync(playwright, browserType, jsEnabled, viewport, BaseUrl);
            //Debug option
            if (_tracingEnabled)
            {
                await browserContext.Tracing.StartAsync(new()
                {
                    Screenshots = true,
                    Snapshots = true,
                    Sources = true
                });
            }


            IPage page = await browserContext.NewPageAsync();

            var logs = new List<string>();

            page.Console += (_, msg) =>
            {
                //if (msg.Type == "log") // could also inspect "info", "error", etc.
                //{
                    logs.Add(msg.Text);
                //}
            };


            //Debug option
            //await page.PauseAsync();

            await page.GotoOnceNetworkIsIdleAsync("TELButtonPage");
            ILocator button = page.GetByRole(AriaRole.Button, new() { Name = "Click Counter" });
            
            await Expect(button).ToContainTextAsync("Button pressed 0 times");
            // 1. Check visibility
            await Expect(button).ToBeVisibleAsync();

          

            //qqqq change to arrange setup assert
            if (jsEnabled)
            {

                // Find the button and click it
                await button.ClickAsync();

                // Check if JavaScript is enabled by verifying the text changes
                await Expect(button).ToContainTextAsync("Button pressed 1 times");
                //await Expect(button).ToContainTextAsync(jsEnabled ? "Button pressed 1 times" : "Button pressed 0 times");


                // Wait a bit if necessary for logs to flush
                await page.WaitForTimeoutAsync(500);

                // ✅ Assert the log appeared
                logs.Should().Contain(log => log.Contains("Button clicked:"));

            }
            else 
            {
                await page.RouteAsync("**/nojsfallback/mvcendpoint/telbuttonshowcase", async route =>
                {
                    var request = route.Request;
                    var postData = request.PostData;
                    Assert.Contains("increment=1", postData);
                    await route.FulfillAsync(new() { Status = 200, Body = "Intercepted OK" });
                });

                await button.ClickAsync();
            }

            if (_tracingEnabled)
            {
                string methodName = "TELButtonPage_InteractivityIsCorrectlySimulated";
                string timestamp = DateTime.UtcNow.ToString("yy_MM_dd_HH_mm_ss", CultureInfo.InvariantCulture);
                string arguments = $"{browserType}_{$"jsEnabled_{jsEnabled.ToString()}"}_{viewport.ToString()}";
                string path = $"../../../Reports/{methodName}_{arguments}_{timestamp}.zip";
                await browserContext.Tracing.StopAsync(new()
                {
                    Path = path,

                });
            }

            // Clean up resources by closing the page and browser context
            await page.CloseAsync();
            await browserContext.CloseAsync();

        }
    } 
}
