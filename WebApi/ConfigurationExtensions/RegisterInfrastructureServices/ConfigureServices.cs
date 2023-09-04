namespace WebApi
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(config =>
            {
                config.UseNpgsql(connectionString: configuration.GetConnectionString("DefaultConnection"))
                .UseLazyLoadingProxies();
            });

            services.AddHttpContextAccessor();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
