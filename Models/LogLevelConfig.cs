public class LogLevelConfig
{
    public string Default { get; set; } = "";
    public Dictionary<string, string> CustomLevels { get; set; } = new();
}