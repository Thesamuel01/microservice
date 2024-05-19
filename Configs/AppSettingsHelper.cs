using static System.Collections.Specialized.BitVector32;

namespace ProductAPI.Configs;

public class AppSettingsHelper : IAppSettingsHelper
{
    private readonly IConfiguration _configuration;

    public AppSettingsHelper(IConfiguration configuration)  
    {
        _configuration = configuration;
    }

    public string? GetAppSettings(string section)
    {
        return _configuration.GetSection(section).Value;
    }

    public string GetConnectionString(string key)
    {
        return _configuration.GetConnectionString(key);
    }
}
