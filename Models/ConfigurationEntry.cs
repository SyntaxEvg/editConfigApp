using Newtonsoft.Json.Linq;

public class ConfigurationEntry
{
    public string Key { get; set; }
    public object Value { get; set; }
    public bool IsObject  { get; set; } //Value is JObject;
    public bool IsArray { get; set; } // Value is JArray;
    public bool IsExpanded { get; set; } = false;
    public List<ConfigurationEntry> Children { get; set; } = new List<ConfigurationEntry>();
}