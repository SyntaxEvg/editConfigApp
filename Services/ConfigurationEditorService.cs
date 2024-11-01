using Newtonsoft.Json.Linq;

namespace editConfigApp.Services
{
    public class ConfigurationEditorService : IConfigurationEditorService
    {
        private readonly string _configPath;

        public ConfigurationEditorService(IWebHostEnvironment env)
        {
            _configPath = Path.Combine(env.ContentRootPath, "appsettings.json");
        }
        public async Task<List<ConfigurationEntry>> LoadConfigurationAsync()
        {
            var json = await File.ReadAllTextAsync(_configPath);
            var jObject = JObject.Parse(json);
            return ConvertToConfigurationEntries(jObject);
        }
        public async Task SaveConfigurationAsync(List<ConfigurationEntry> configuration)
        {
            var jObject = ConvertToJObject(configuration);
            var json = jObject.ToString(Newtonsoft.Json.Formatting.Indented);
            await File.WriteAllTextAsync(_configPath, json);
        }
        private List<ConfigurationEntry> ConvertToConfigurationEntries(JObject jObject)
        {
            var entries = new List<ConfigurationEntry>();
            foreach (var property in jObject.Properties())
            {
                var entry = new ConfigurationEntry
                {
                    Key = property.Name,
                    Value = property.Value
                };

                if (property.Value is JObject childObject)
                {
                    entry.IsObject = true;
                    entry.Children = ConvertToConfigurationEntries(childObject);
                }
                else if (property.Value is JArray array)
                {
                    entry.IsArray = true;
                    // Обработка массивов
                    entry.Children = array.Select((item, index) => new ConfigurationEntry
                    {
                        Key = index.ToString(),
                        Value = item
                    }).ToList();
                }

                entries.Add(entry);
            }
            return entries;
        }
        private JObject ConvertToJObject(List<ConfigurationEntry> entries)
        {
            var jObject = new JObject();
            foreach (var entry in entries)
            {
                if (entry.IsObject)
                {
                    jObject[entry.Key] = ConvertToJObject(entry.Children);
                }
                else if (entry.IsArray)
                {
                    jObject[entry.Key] = new JArray(entry.Children.Select(c => c.Value));
                }
                else
                {
                    jObject[entry.Key] = JToken.FromObject(entry.Value);
                }
            }
            return jObject;
        }
    }
}
