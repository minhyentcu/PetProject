using BaseSource.ApiIntegration.WebApi.Category;
using BaseSource.ApiIntegration.WebApi.Project;
using BaseSource.ApiIntegration.WebApi.ProjectClient;
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

            services.AddTransient<IProjectAdminApiClient, ProjectAdminApiClient>();
            services.AddTransient<IProjectApiClient, ProjectApiClient>();
            services.AddTransient<ICategoryService, CategoryService>();

            return services;
        }
    }
}
