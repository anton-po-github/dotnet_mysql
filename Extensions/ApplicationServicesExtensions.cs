public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        //  services.AddScoped<ITokenService, TokenService>();
        // services.AddScoped<IUserService, UserService>();

        return services;
    }
}
