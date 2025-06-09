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
        public static IServiceCollection AddTELBlazorComponentServices(this IServiceCollection services,
            bool IsClient,
            ITELBlazorBaseComponentConfiguration TELBlazorBaseComponentConfiguration
            )
        {
            throw new NotImplementedException("This method is not implemented yet. See DI task");
        }
    }
}
