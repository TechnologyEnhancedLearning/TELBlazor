using Serilog.Core;
using Serilog.Events;
using Serilog;
using TELBlazor.Components.Core.Configuration;
using TELBlazor.Components.Core.Services.HelperServices;
using TELBlazor.Components.ShowCase.E2ETests.WasmServerHost;
using TELBlazor.Components.ShowCase.Shared.Services.HelperServices;

using Microsoft.Extensions.Http;


using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
/*Are used via appsetting*/
using Serilog.Extensions.Logging;
using Serilog.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Sinks.BrowserConsole;
using Serilog.Formatting.Compact;
using Serilog.Settings.Configuration;
using Microsoft.AspNetCore.Components;
/*qqqq setup*/
using Blazored.LocalStorage;


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
    //qqqq for di?
    builder.Services.AddBlazoredLocalStorage();

    //qqqq cant remember what for
    //builder.Services.AddHttpContextAccessor();
    //builder.Services.AddSession(options =>
    //{

    //    options.IdleTimeout = TimeSpan.FromMinutes(20); // Set the timeout for the session
    //    options.Cookie.HttpOnly = true; // Session cookie is only accessible via HTTP
    //    options.Cookie.IsEssential = true; // Session cookie is essential for application
    //});

    builder.Services.AddSingleton<ITELBlazorBaseComponentConfiguration>(provider =>
    {
        // qqqq cant rmeember purpose
        //In here we would get our appsettings etc and configure - but then we have an object to pass it 
        //var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
        ////For this to work well for blazor pages as well we would probably want to set up session storage (which takes js anyway to set up and most blazor application use it i presume)
        //var session = httpContextAccessor.HttpContext?.Session;
        
        
        return new TELBlazorBaseComponentConfiguration
        {
            JSEnabled = true, //if we are inject the client then it is true
            HostType = $"{builder.Configuration["Properties:Environment"]} {builder.Configuration["Properties:Application"]}"
        };
    });
    //Scoped because being consumed with storage where singleton doesnt survive mvc page teardown
    builder.Services.AddScoped<LoggingLevelSwitch>(sp => levelSwitch);
    builder.Services.AddScoped<ILogLevelSwitcherService, SerilogLogLevelSwitcherService>();
    var app = builder.Build();
    app.UseSerilogRequestLogging();
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





