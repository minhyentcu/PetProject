using BaseSource.ApiIntegration.WebApi.Setting;
using BaseSource.ApiIntegration.WebApi.User;
using BaseSource.ApiIntegration.WebApi.UserAdmin;

namespace BaseSource.AppUI.Configuration
{
    public static class DIConfigurations
    {
        public static IServiceCollection DIConfiguration(this IServiceCollection services)
        {
            //DI for user
            services.AddTransient<IUserApiClient, UserApiClient>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IConfigSettingApiClient, ConfigSettingApiClient>();

            services.AddTransient<IUserAdminApiClient, UserAdminApiClient>();

            return services;
        }
    }
}
