using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container

var services = builder.Services;
var env = builder.Environment;

// 1. Configure CORS to allow both local dev and production Angular apps
services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200", "https://ng-dotnet.web.app")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

services.AddSwaggerGen();

services.AddDbContext<UsersContext>();
services.AddDbContext<ProductsContext>();
services.AddDbContext<IdentityContext>();

services.AddIdentityServices(builder.Configuration);

services.AddScoped<TokenService>();
services.AddScoped<UserService>();
services.AddScoped<ProductsService>();

services.AddCors();
services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    // ignore omitted parameters on models to enable optional params (e.g. User update)
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var sp = scope.ServiceProvider;
    var logger = sp.GetRequiredService<ILogger<Program>>();
    try
    {
        // Apply pending migrations only in Development environment
        var contexts = new DbContext[]
        {
            sp.GetRequiredService<UsersContext>(),
            sp.GetRequiredService<IdentityContext>(),
            sp.GetRequiredService<ProductsContext>(),
        };

        foreach (var ctx in contexts)
        {
            var pending = ctx.Database.GetPendingMigrations();
            if (pending.Any())
            {
                logger.LogInformation("Applying {Count} pending migrations for {Context}", pending.Count(), ctx.GetType().Name);
                ctx.Database.Migrate();
                logger.LogInformation("Migrations applied to {Context}", ctx.GetType().Name);
            }
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error applying migrations in Development");
    }
}

using (var scopeRole = app.Services.CreateScope())
{
    await IdentitySeeder.SeedRolesAsync(scopeRole.ServiceProvider);
}

using (var scope = app.Services.CreateScope())
{
    var servicesProvider = scope.ServiceProvider;
    var logger = servicesProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        var userManager = servicesProvider.GetRequiredService<UserManager<IdentityUser>>();

        await IdentityContextSeed.SeedUsersAsync(userManager);
        await IdentitySeeder.SeedRolesAsync(servicesProvider);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred during database migration or seeding");
    }
}

// 12. Swagger UI & HTTPS redirection in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

app.Run();

























/* var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
 */