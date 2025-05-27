using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TELBlazor.Components.Core.Configuration
{
    public class TELBlazorBaseComponentConfiguration : ITELBlazorBaseComponentConfiguration
    {
        public bool JSEnabled { get; set; } = false;

        //E.g. Injected from the client or server
        public string HostType { get; set; } = "Unset";


    }
}
