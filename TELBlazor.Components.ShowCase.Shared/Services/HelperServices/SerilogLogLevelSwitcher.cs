using Microsoft.Extensions.Logging;
using Serilog.Core;
using Serilog.Events;
using TELBlazor.Components.Core.Services.HelperServices;
using Blazored.LocalStorage;
using TELBlazor.Components.Core.Models.Logging;

namespace TELBlazor.Components.ShowCase.Shared.Services.HelperServices
{
    /// <summary>
    /// qqqq needs review
    /// This quite likely should be our BlazorLogger service and logger should be called from it and TELComponentBase should hold it
    /// </summary>
    public class SerilogLogLevelSwitcherService : ILogLevelSwitcherService
    {
        private readonly LoggingLevelSwitch _loggingLevelSwitch;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private readonly ILocalStorageService _localStorage;
        private const string LogLevelKey = "logLevel";


        public bool IsInitialized { get; set; } = false;

        public SerilogLogLevelSwitcherService(
            LoggingLevelSwitch loggingLevelSwitch,
            Microsoft.Extensions.Logging.ILogger<SerilogLogLevelSwitcherService> logger,
            ILocalStorageService localStorage)
        {
            _loggingLevelSwitch = loggingLevelSwitch;
            _logger = logger;
            _localStorage = localStorage;
            //InitializeLogLevelFromAsyncSourceIfAvailable(); Must be called from the component but the component is supposted to be agnostic

        }
        public async Task InitializeLogLevelFromAsyncSourceIfAvailable()
        {
            if (IsInitialized) return;

            try
            {
                string storedLevel = await GetStoredLogLevelWithExpiration();
                if (!string.IsNullOrEmpty(storedLevel))
                {
                    if (Enum.TryParse(storedLevel, true, out LogEventLevel logLevel) && logLevel > _loggingLevelSwitch.MinimumLevel)
                    {
                        SetLogLevel(logLevel.ToString());
                        _logger.LogInformation("Log level initialized from local storage: {Level}", logLevel);
                    }
                }
                IsInitialized = true;
            }
            catch (Exception ex)
            {
                //We would do prerender check here if it is serve prerender there is no storage
                _logger.LogError(ex, "Error initializing log level from local storage.");
            }
        }

        public List<string> GetAvailableLogLevels() => Enum.GetNames(typeof(LogEventLevel)).ToList();

        public string GetCurrentLogLevel()
        {
            string logLevel = _loggingLevelSwitch.MinimumLevel.ToString();
            _logger.LogInformation("Fetching current log level: {Level}", logLevel);

            return logLevel;
        }

        public string SetLogLevel(string level)
        {
            LogAllLevels("Before Change");
            if (string.IsNullOrWhiteSpace(level))
            {
                _logger.LogWarning("Attempted to set log level with an empty value.");
            }

            if (!Enum.TryParse(level, true, out LogEventLevel logLevel))
            {
                _logger.LogWarning("Invalid log level received: {Level}", level);

            }

            _logger.LogInformation("Changing log level from {OldLevel} to {NewLevel}",
                                    _loggingLevelSwitch.MinimumLevel.ToString(), logLevel.ToString());

            _loggingLevelSwitch.MinimumLevel = logLevel;
            StoreLogLevelWithTimestamp(logLevel.ToString());
            LogAllLevels("After Change");
            return GetCurrentLogLevel();
        }

        // Logs a message at all log levels to test visibility
        private void LogAllLevels(string phase)
        {
            _logger.LogTrace("[{Phase}] TRACE level log", phase);
            _logger.LogDebug("[{Phase}] DEBUG level log", phase);
            _logger.LogInformation("[{Phase}] INFORMATION level log", phase);
            _logger.LogWarning("[{Phase}] WARNING level log", phase);
            _logger.LogError("[{Phase}] ERROR level log", phase);
            _logger.LogCritical("[{Phase}] CRITICAL level log", phase);
        }


        private async Task<string> GetStoredLogLevelWithExpiration()
        {
            try
            {
                LocalStorageLogLevel? storedItem = await _localStorage.GetItemAsync<LocalStorageLogLevel>(LogLevelKey);


                if (storedItem == null)
                {
                    return null;
                }


                if (DateTime.UtcNow > storedItem.Expires)
                {
                    await _localStorage.RemoveItemAsync(LogLevelKey);

                    return null;
                }

                return storedItem.Level;
            }
            catch (Exception)
            {
                return null; // Return null if local storage access fails
            }
        }

        private async Task StoreLogLevelWithTimestamp(string level)
        {
            try
            {
                LocalStorageLogLevel? storedItem = await _localStorage.GetItemAsync<LocalStorageLogLevel>(LogLevelKey);
                if (storedItem != null && (DateTime.UtcNow < storedItem.Expires || storedItem.Level == level))
                {
                    if (storedItem != null && (DateTime.UtcNow < storedItem.Expires))
                    {
                        // Expired or different level: delete 
                        await _localStorage.RemoveItemAsync(LogLevelKey); // Remove the old item
                    }

                    //its already set and we dont want to extend expiry
                    return;
                }
                else
                {
                    var newItem = new LocalStorageLogLevel
                    {
                        Level = level,
                        Expires = DateTime.UtcNow.AddHours(24)
                    };

                    await _localStorage.SetItemAsync(LogLevelKey, newItem);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error storing log level to local storage.");
            }
        }


    }
}
