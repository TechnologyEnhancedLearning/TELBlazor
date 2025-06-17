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
            IBrowser browser;

            switch (browserType.ToLower())
            {
                case "chromium":
                    browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions {  });
                    break;
                case "firefox":
                    browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions {  });
                    break;
                case "webkit":
                    browser = await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { });
                    break;
                default:
                    throw new ArgumentException($"Unsupported browser type: {browserType}");
            }


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
