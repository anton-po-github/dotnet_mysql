using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class IdentityController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly TokenService _tokenService;
    private readonly IMapper _mapper;
    public IdentityController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, TokenService tokenService, IMapper mapper)
    {
        _mapper = mapper;
        _tokenService = tokenService;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("secret")]
    public IActionResult SecretEndpoint()
    {
        return Ok("You are an admin, congratulations!");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await _userManager.FindByEmailFromClaimsPrinciple(User);

        return new UserDto
        {
            Email = user.Email,
            Token = await _tokenService.CreateToken(user),
            UserName = user.UserName,
            Role = await _userManager.GetRolesAsync(user)
        };
    }

    [Authorize(Roles = "User")]
    [HttpGet("all")]
    public async Task<ActionResult<List<IdentityUser>>> GetAllUsers()
    {
        return await _userManager.Users.ToListAsync();
    }

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }


    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null) return Unauthorized(new ApiResponse(401));

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

        return new UserDto
        {
            Email = user.Email,
            Token = await _tokenService.CreateToken(user),
            UserName = user.UserName,
            Role = await _userManager.GetRolesAsync(user)
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        var user = new IdentityUser
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
        };

        if (_userManager.Users.Any(x => x.Email == registerDto.Email))
            throw new AppException("User with the email '" + registerDto.Email + "' already exists");

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded) return BadRequest(new ApiResponse(400));

        var addUserToRoleResult = await _userManager.AddToRoleAsync(user, "Admin");
        if (!addUserToRoleResult.Succeeded) throw new AppException($"Create user succeeded but could not add user to role {addUserToRoleResult?.Errors?.First()?.Description}");

        return new UserDto
        {
            Email = user.Email,
            Token = await _tokenService.CreateToken(user),
            UserName = user.UserName,
            Role = await _userManager.GetRolesAsync(user)
        };
    }

    [HttpDelete("{id}")]
    public async Task<IdentityResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        var result = await _userManager.DeleteAsync(user);

        return result;

    }
}
