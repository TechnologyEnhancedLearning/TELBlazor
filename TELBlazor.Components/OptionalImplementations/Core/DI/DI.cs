//using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TELBlazor.Components.Core.Configuration;

namespace TELBlazor.Components.OptionalImplementations.Core.DI
{
    public static class DI
    {
        private static IServiceCollection AddTELBlazorComponentServicessShared(this IServiceCollection services,
            ITELBlazorBaseComponentConfiguration TELBlazorBaseComponentConfiguration
            )
        {
            throw new NotImplementedException("This method is not implemented yet. See DI task");
        }
        public static IServiceCollection AddTELBlazorComponentServicesClient(this IServiceCollection services,

            ITELBlazorBaseComponentConfiguration TELBlazorBaseComponentConfiguration
            )
        {
            throw new NotImplementedException("This method is not implemented yet. See DI task");
        }
        public static IServiceCollection AddTELBlazorComponentServicesServer(this IServiceCollection services,

            ITELBlazorBaseComponentConfiguration TELBlazorBaseComponentConfiguration
            )
        {
            throw new NotImplementedException("This method is not implemented yet. See DI task");
        }
    }
}
