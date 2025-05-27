using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TELBlazor.Components.Core.Configuration
{
    public interface ITELBlazorBaseComponentConfiguration
    {
        public bool JSEnabled { get; set; }

        //E.g. Injected from the client or server
        public string HostType { get; set; }
    }
}
