﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TELBlazor.Components.ShowCase.E2ETests.Helpers
{
    public static class ViewportHelper
    {
        public enum ViewportType
        {
            Desktop,
            Mobile,
            Tablet
        }

        public static readonly Dictionary<ViewportType, ViewportSize> Viewports = new()
        {
            //qqqqq ask alan what to test against
            { ViewportType.Desktop, new ViewportSize { Width = 1920, Height = 1080 } },
            { ViewportType.Mobile, new ViewportSize { Width = 375, Height = 812 } }, // iPhone X
            { ViewportType.Tablet, new ViewportSize { Width = 768, Height = 1024 } } // iPad
        };
    }
}
