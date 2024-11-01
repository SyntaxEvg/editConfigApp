using editConfigApp.Components;
using editConfigApp.Services;
using Microsoft.AspNetCore.Server.IISIntegration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        builder.Services.AddScoped<IConfigService, ConfigService>();
        builder.Services.AddScoped<IConfigurationEditorService, ConfigurationEditorService>();
        //builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AllowedEmails"));
     //   builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);
     ////   builder.Services.AddAuthorization();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IUserAccessService, UserAccessService>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
        }

        app.UseStaticFiles();
        //app.UseHttpsRedirection();
        app.UseAntiforgery();
        //app.UseAuthentication();
        //app.UseAuthorization();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
