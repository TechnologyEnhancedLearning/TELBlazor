using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TELBlazor.Components.ShowCase.E2ETests.Helpers.ViewportHelper;

namespace TELBlazor.Components.ShowCase.E2ETests.Helpers
{
    public static class BrowserHelper
    {

        public static async Task<IBrowserContext> CreateBrowserContextAsync(IPlaywright playwright, string browserType, bool jsEnabled, ViewportType viewport, string baseUrl)
        {

            IBrowser browser = browserType.ToLower() switch
            {
                "chromium" => await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { }),
                "firefox" => await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { }),
                "webkit" => await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { }),
                _ => throw new ArgumentException($"Unsupported browser type: {browserType}")
            };


            BrowserNewContextOptions contextOptions = new BrowserNewContextOptions
            {

                JavaScriptEnabled = jsEnabled,
                BaseURL = baseUrl,
                IgnoreHTTPSErrors = true,
                ViewportSize = ViewportHelper.Viewports[viewport]
            };
            IBrowserContext context = await browser.NewContextAsync(contextOptions);

            return context;
        }

    }
}
