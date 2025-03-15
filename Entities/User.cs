public class IdentityUser : Microsoft.AspNetCore.Identity.IdentityUser { }

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    // public Byte[]? PhotoUrl { get; set; }
}

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
public class RegisterDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UserDto
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Token { get; set; }
}