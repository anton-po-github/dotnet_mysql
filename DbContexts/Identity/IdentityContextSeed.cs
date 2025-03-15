using Microsoft.AspNetCore.Identity;

public class IdentityContextSeed
{
    public static async Task SeedUsersAsync(UserManager<IdentityUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new IdentityUser
            {
                UserName = "Bob",
                Email = "bob@test.com"
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }
}
