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
        // Setting value using PackageSetting.props potentially replace appsettings.Test.json in future especially if apis
        static bool headless =>
        #if HEADLESS_TESTING
                        true;
        #else
                        false;
        #endif

        public static async Task<IBrowserContext> CreateBrowserContextAsync(IPlaywright playwright, string browserType, bool jsEnabled, ViewportType viewport, string baseUrl)
        {

            IBrowser browser;

            bool headless = (bool.TryParse(Environment.GetEnvironmentVariable("HEADLESS_TESTING"), out var result) && result);
            switch (browserType.ToLower())
            {
                case "chromium":
                    browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = headless });
                    break;
                case "firefox":
                    browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = headless });
                    break;
                case "webkit":
                    browser = await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = headless });
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
