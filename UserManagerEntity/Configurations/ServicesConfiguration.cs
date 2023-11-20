using UserManagerEntity.Repository;
using UserManagerEntity.Services;
using static UserManagerEntity.Services.UserServices;

namespace UserManagerEntity.Configurations;
/// <summary>
/// Class for add services
/// </summary>
public static class ServicesConfiguration
{
    /// <summary>
    /// Define service here
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUserServices, UserServices>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAccountRepository, AccountRepository>();

        services.AddControllers(option =>
        {
            option.Filters.Add(typeof(CustomExceptionFilter));
        });
    }
}

