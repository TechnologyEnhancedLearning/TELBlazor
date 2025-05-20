using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TELBlazor.Components.Core.Models.Logging
{
    public interface ILocalStorageLogLevel
    {
        public string Level { get; set; } 
        public DateTime Expires { get; set; }
    }
}
