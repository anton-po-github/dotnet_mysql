using Microsoft.EntityFrameworkCore;

public class ProductsContext : DbContext
{
    protected readonly IConfiguration _config;
    public ProductsContext(IConfiguration config)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = _config.GetConnectionString("ProductDatabase");

        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).

        UseSnakeCaseNamingConvention();
    }

    public DbSet<Product> Products { get; set; }
}
