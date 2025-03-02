using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
public class IdentityContext : IdentityDbContext<AppUser>
{
    protected readonly IConfiguration _config;

    public IdentityContext(IConfiguration configuration)
    {
        _config = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = _config.GetConnectionString("IdentityConnection");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).
        UseSnakeCaseNamingConvention();
    }

    public DbSet<AppUser> Identity { get; set; }
}