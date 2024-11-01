namespace editConfigApp.Services
{
    public interface IConfigurationEditorService
    {
        Task<List<ConfigurationEntry>> LoadConfigurationAsync();
        Task SaveConfigurationAsync(List<ConfigurationEntry> configuration);
    }
}
