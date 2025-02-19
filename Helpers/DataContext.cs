namespace WebApi.Helpers;

using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration _config;

    public DataContext(IConfiguration configuration)
    {
        _config = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to mysql with connection string from app settings

        var connectionString = _config.GetConnectionString("ApiDatabase");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).
        UseSnakeCaseNamingConvention();
    }

    public DbSet<User> Users { get; set; }
}