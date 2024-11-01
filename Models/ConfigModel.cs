public class ConfigModel
{
    public LoggingConfig Logging { get; set; } = new();
    public string AllowedHosts { get; set; } = "";
}
