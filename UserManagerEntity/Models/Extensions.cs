using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using UserManagerEntity.Models.Entities;
using UserManagerEntity.Settings;

namespace UserManagerEntity.Models;
public static class Extensions
{
    public static async void CreateDbIfNotExists(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<DataContext>();
            var roleManager = services.GetRequiredService<RoleManager<Role>>();
            var userManager = services.GetRequiredService<UserManager<User>>();
            var identitySettings = services.GetRequiredService<IdentitySettings>();
            // var roleStore = new RoleStore<Role>(context);

            var defaultUser = new User()
            {
                UserName = identitySettings.DefaultUserName,
            };
            var defaultPassword = identitySettings.DefaultPassword;

            var createdUserResult = await userManager.CreateAsync(defaultUser, defaultPassword);
            if(createdUserResult == null)
            {
                //
            }
        }
    }
    public static bool IsEmail(string email)
    {
        if (email == null) return false;
        try
        {
            MailAddress m = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
