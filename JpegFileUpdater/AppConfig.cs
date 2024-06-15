using System.Text.Json;
using System.Text.Json.Serialization;

namespace JpegFileUpdater;

internal class AppConfig
{
    private static string ConfigPath
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), APP_NAME, "appconfig.json");

    [JsonInclude]
    internal string ExifToolPath { get; set; } = string.Empty;

    [JsonInclude]
    internal string BaseFileName { get; set; } = string.Empty;

    [JsonInclude]
    internal int Digit { get; set; } = 3;

    [JsonInclude]
    internal int StartNumber { get; set; } = 1;

    [JsonInclude]
    internal DateTime BaseDateTime { get; set; } = new DateTime(DateTime.Now.Year, 1, 1);

    [JsonInclude]
    internal int Seconds { get; set; } = 1;

    internal static AppConfig Load()
        => File.Exists(ConfigPath) ? JsonSerializer.Deserialize<AppConfig>(File.ReadAllText(ConfigPath)) ?? new AppConfig() : new AppConfig();

    private void Save()
    {
        new FileInfo(ConfigPath).Directory?.Create();
        File.WriteAllText(ConfigPath, JsonSerializer.Serialize(this));
    }

    internal static void SaveExifToolPath(string path)
    {
        var config = Load();
        config.ExifToolPath = path;
        config.Save();
    }

    internal static void SaveBaseFileName(string name)
    {
        var config = Load();
        config.BaseFileName = name;
        config.Save();
    }

    internal static void SaveDigit(int digit)
    {
        var config = Load();
        config.Digit = digit;
        config.Save();
    }

    internal static void SaveStartNumber(int number)
    {
        var config = Load();
        config.StartNumber = number;
        config.Save();
    }

    internal static void SaveBaseDateTime(DateTime dateTime)
    {
        var config = Load();
        config.BaseDateTime = dateTime;
        config.Save();
    }

    internal static void SaveSeconds(int seconds)
    {
        var config = Load();
        config.Seconds = seconds;
        config.Save();
    }
}
