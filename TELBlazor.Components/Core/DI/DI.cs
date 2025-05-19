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

namespace TELBlazor.Components.Core.DI
{
    public static class DI
    {
        /// <summary>
        /// Adding this to your program.cs will enable Visual Studio to notify you when a package update requires a new service adding
        /// </summary>
        /// <details>
        /// qqqq what if its already injected we will then be requiring a second injection
        /// Excludes
        /// </details>
        /// <param name="services"></param>
        /// <param name="IsClient">client in some occassions will have different scoping</param>
        /// <returns></returns>
        public static IServiceCollection AddTELBlazorComponentServices(this IServiceCollection services, 
            bool IsClient,
            ITELBlazorBaseComponentConfiguration TELBlazorBaseComponentConfiguration
            )
        {

            if (IsClient)
            {
                services.AddSingleton<ITELBlazorBaseComponentConfiguration>(sp =>{return TELBlazorBaseComponentConfiguration;});

                //services.AddSingleton<ITELBlazorBaseComponentConfiguration>(sp =>
                //{
                //    return new TELBlazorBaseComponentConfiguration
                //    {
                //        JSEnabled = true, //if we are inject the client then it is true
                //        HostType = $"{builder.Configuration["Properties:Environment"]} {builder.Configuration["Properties:Application"]}"
                //    };
                //});
            }
            else 
            {
                services.AddScoped<ITELBlazorBaseComponentConfiguration>(sp => { return TELBlazorBaseComponentConfiguration; });
            }

                //Add Services
                //qqqq not uing this yet, services.AddBlazoredLocalStorage();
                //qqqq come back and refactor client a service to use this
                // qqqq if the controllers already have the service then this would require another to be added and then there would be two the later overides the former but seems innefficient
                // we may require a service thats never used if the consuming project doesnt use it, so it seems it would be useful for lh but actually thats it, or we make them optional.
             return services;
        }
    }
}
