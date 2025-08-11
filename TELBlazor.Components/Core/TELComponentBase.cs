using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using TELBlazor.Components.Core.Configuration;

namespace TELBlazor.Components.Core
{
    /// <summary>
    /// Base class for TEL Blazor components.
    /// Provides shared configuration and logging functionality.
    /// </summary>
    public class TELComponentBase : ComponentBase
    {
        // this will receive server version prerender and then client side if received must be true
        [Inject]
        private ITELBlazorBaseComponentConfiguration TELBlazorBaseComponentConfiguration { get; set; } = default!;

        [Inject]
        public ILogger<TELComponentBase> BaseLogger { get; set; } = default!;

        protected bool JSEnabled => TELBlazorBaseComponentConfiguration.JSEnabled;
        protected string HostType => TELBlazorBaseComponentConfiguration.HostType;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            BaseLogger.LogInformation("TEL base component initialised made by {HostType}, JsEnabled is {JsEnabled}", HostType, JSEnabled);
        }
    }
}
