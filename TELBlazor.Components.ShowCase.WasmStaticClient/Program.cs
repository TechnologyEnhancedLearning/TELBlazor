using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TELBlazor.Components.ShowCase.WasmStaticClient;
using Microsoft.AspNetCore.Components;

/*Are used via appsetting*/
using Serilog.Extensions.Logging;
using Serilog.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Sinks.BrowserConsole;
using Serilog.Formatting.Compact;
using Serilog.Settings.Configuration;



/*qqqq setup*/
using Blazored.LocalStorage;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using TELBlazor.Components.Core.Services.HelperServices;
using TELBlazor.Components.ShowCase.Shared.Services.HelperServices;
using TELBlazor.Components.Core.Configuration;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile($"appsettings.{builder.HostEnvironment.Environment}.json", optional: false, reloadOnChange: true);

builder.Logging.ClearProviders();
// Read default logging level from configuration
var logLevelString = builder.Configuration["Serilog:MinimumLevel:Default"];
// Convert string to LogEventLevel (with fallback)
if (!Enum.TryParse(logLevelString, true, out LogEventLevel defaultLogLevel))
{
    defaultLogLevel = LogEventLevel.Information; // Default if parsing fails
}

// Create a LoggingLevelSwitch that can be updated dynamically
LoggingLevelSwitch levelSwitch = new LoggingLevelSwitch(defaultLogLevel); // Default: Information added this so in production can change the logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .MinimumLevel.ControlledBy(levelSwitch)
    .CreateLogger();

// Add Serilog to logging providers
builder.Logging.AddSerilog(Log.Logger, dispose: true);

//for really bad fails
try
{
    builder.Services.AddSingleton<ITELBlazorBaseComponentConfiguration>(sp =>
    {
        return new TELBlazorBaseComponentConfiguration
        {
            JSEnabled = true, //if we are inject the client then it is true
            HostType = $"{builder.Configuration["Properties:Environment"]} {builder.Configuration["Properties:Application"]}"
        };
    });
    // qqqq to DI
    builder.Services.AddBlazoredLocalStorage();

    //Scoped because being consumed with storage where singleton doesnt survive mvc page teardown
    builder.Services.AddScoped<LoggingLevelSwitch>(sp => levelSwitch);
    builder.Services.AddScoped<ILogLevelSwitcherService, SerilogLogLevelSwitcherService>();
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    //If in production as requires sending to api we may never receive it
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush(); // Ensure logs are flushed before exit
}