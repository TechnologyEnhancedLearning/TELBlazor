using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TELBlazor.Components.Core.Configuration;

namespace TELBlazor.Components.Core
{
    public class TELComponentBase : ComponentBase
    {
        // this will receive server version prerender and then client side if received must be true
        [Inject]
        private ITELBlazorBaseComponentConfiguration TELBlazorBaseComponentConfiguration { get; set; }

        [Inject]
        public required ILogger<TELComponentBase> Logger { get; set; }

        protected bool JSEnabled => TELBlazorBaseComponentConfiguration.JSEnabled;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Logger.LogInformation("base component made by {HostType}", TELBlazorBaseComponentConfiguration.HostType);
        }
    }
}
