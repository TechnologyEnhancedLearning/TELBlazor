// Microsoft namespaces
// Still required server side even if not used so components dont fail
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
// Serilog core (used via appsettings, do not delete even if vs marks not in use)
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
// Serilog extensions and sinks (used via appsettings, do not delete even if vs marks not in use)
using Serilog.Extensions.Logging;
using Serilog.Formatting.Compact;
using Serilog.Settings.Configuration;
using Serilog.Sinks.BrowserConsole;
// TELBlazor components
using TELBlazor.Components.Core.Configuration;
using TELBlazor.Components.Core.Services.HelperServices;
using TELBlazor.Components.OptionalImplementations.Core.DI;
using TELBlazor.Components.OptionalImplementations.Core.Services.HelperServices;
using TELBlazor.Components.ShowCase.E2ETests.WasmServerHost;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

builder.Configuration.AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true);

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
builder.Logging.AddSerilog(Log.Logger, dispose: true);//qqqq may not need dispose for client

//for really bad fails
try
{
    // Candidates for DI collection
    builder.Services.AddSingleton<ITELBlazorBaseComponentConfiguration>(sp =>
    {
        return new TELBlazorBaseComponentConfiguration
        {
            JSEnabled = true, //if we are inject the client then it is true
            HostType = $"{builder.Configuration["Properties:Environment"]} {builder.Configuration["Properties:Application"]}"
        };
    });

    
    builder.Services.AddBlazoredLocalStorage();

    //Scoped because being consumed with storage where singleton doesnt survive mvc page teardown
    // qqqq do we need it builder.Services.AddScoped<LoggingLevelSwitch>(sp => levelSwitch);
    builder.Services.AddScoped<ILogLevelSwitcherService, SerilogLogLevelSwitcherService>();
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
    builder.Services.AddTELBlazorComponentServicesForTestComponents();
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