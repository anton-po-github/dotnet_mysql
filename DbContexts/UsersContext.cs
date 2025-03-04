using Microsoft.EntityFrameworkCore;
public class UsersContext : DbContext
{
    protected readonly IConfiguration _config;

    public UsersContext(IConfiguration configuration)
    {
        _config = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = _config.GetConnectionString("UsersDatabase");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).
        UseSnakeCaseNamingConvention();
    }

    public DbSet<User> Users { get; set; }
}