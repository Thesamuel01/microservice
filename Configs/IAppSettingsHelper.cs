namespace ProductAPI.Configs;

public interface IAppSettingsHelper
{
    string? GetAppSettings(string section);

    string GetConnectionString(string key);
}
