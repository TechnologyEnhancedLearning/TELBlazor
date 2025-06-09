// Microsoft namespaces
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

// Still required server side even if not used so components dont fail
using Blazored.LocalStorage;

// TELBlazor components
using TELBlazor.Components.Core.Configuration;
using TELBlazor.Components.Core.Services.HelperServices;
using TELBlazor.Components.ShowCase.E2ETests.WasmServerHost;
using TELBlazor.Components.ShowCase.Shared.Services.HelperServices;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
builder.Configuration.AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true);
//builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);
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
builder.Host.UseSerilog();

try
{

    // Add services to the container.
    builder.Services.AddRazorComponents()
        .AddInteractiveWebAssemblyComponents();

    // Future candidates for DI collection
    builder.Services.AddBlazoredLocalStorage();

    builder.Services.AddSingleton<ITELBlazorBaseComponentConfiguration>(provider =>
    {
        return new TELBlazorBaseComponentConfiguration
        {
            JSEnabled = true, //See mvcblazor logic for proper implementation
            HostType = $"{builder.Configuration["Properties:Environment"]} {builder.Configuration["Properties:Application"]}"
        };
    });

    //Scoped because being consumed with storage where singleton doesnt survive mvc page teardown
    builder.Services.AddScoped<LoggingLevelSwitch>(sp => levelSwitch);
    builder.Services.AddScoped<ILogLevelSwitcherService, SerilogLogLevelSwitcherService>();


    var app = builder.Build();
    // This is less blazor more endpoint logging
    //app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseWebAssemblyDebugging();
    }
    else
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();
    app.UseAntiforgery();

    //We want everything from the blazor project so we add by assembly and put all using statements in the imports
    app.MapRazorComponents<App>()
        .AddInteractiveWebAssemblyRenderMode()
        .AddAdditionalAssemblies(typeof(TELBlazor.Components.ShowCase.E2ETests.WasmServerHost.Client._Imports).Assembly)
        .AddAdditionalAssemblies(typeof(TELBlazor.Components.ShowCase.Shared._Imports).Assembly);
    app.Run();
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
// Makes the web app entry point public to
// allow BlazorApplicationFactory/WebApplicationFactory
// to reference it.
public partial class Program { }





