using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TELBlazor.Components.Core.Services.HelperServices
{
    public interface ILogLevelSwitcherService
    {

        /// <summary>
        /// String so can be more generic
        /// </summary>
        /// <returns></returns>
        public string GetCurrentLogLevel();

        public string SetLogLevel(string level);

        public List<string> GetAvailableLogLevels();
        public bool IsInitialized { get; set; }
        public Task InitializeLogLevelFromAsyncSourceIfAvailable();
        //constructor cant call a db or local storage because async so needs doing in async
        //So would need to be in the base component async constructor - seems expensive
    }

}
