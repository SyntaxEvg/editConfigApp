using Newtonsoft.Json;
using System.Xml;

namespace editConfigApp.Services
{
    public interface IConfigService
    {
        Task<ConfigModel> GetConfigAsync();
        Task SaveConfigAsync(ConfigModel config);
    }

    public class ConfigService : IConfigService
    {
        private readonly IWebHostEnvironment _env;

        public ConfigService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<ConfigModel> GetConfigAsync()
        {
            var configPath = Path.Combine(_env.ContentRootPath, "appsettings.json");
            var json = await File.ReadAllTextAsync(configPath);
            return JsonConvert.DeserializeObject<ConfigModel>(json);
        }

        public async Task SaveConfigAsync(ConfigModel config)
        {
            var configPath = Path.Combine(_env.ContentRootPath, "appsettings.json");
            var json = JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented);
            await File.WriteAllTextAsync(configPath, json);
        }
    }
}
