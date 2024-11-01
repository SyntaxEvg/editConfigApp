using System.Security.Principal;

public class UserAccessService : IUserAccessService
{
    private readonly IConfiguration _configuration;
   // private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessService(IConfiguration configuration
       // IHttpContextAccessor httpContextAccessor
        )
    {
        _configuration = configuration;
    //    _httpContextAccessor = httpContextAccessor;
    }

    public bool IsUserAllowed()
    {
        var allowedUsers = _configuration.GetSection("AllowedEmails").Get<List<string>>();
        //var identity = _httpContextAccessor.HttpContext.User.Identity as WindowsIdentity;

        WindowsIdentity identity = WindowsIdentity.GetCurrent();
        //Log.Information($"account: {identity.Name.Split('\\')[0]}\\{identity.Name.Split('\\')[1]}");


        if (identity == null)
            return false;

        var userEmail = identity.Name; // Это будет в формате DOMAIN\username

        // Преобразуем DOMAIN\username в email формат, если это необходимо
        // Например: DOMAIN\john.doe -> john.doe@yourdomain.com
        //var emailPart = userEmail.Split('\\').Last();
        //var fullEmail = $"{emailPart}@yourdomain.com";

        //return allowedUsers.Contains(fullEmail, StringComparer.OrdinalIgnoreCase);
        return true;
    }
}