using AspNetCoreIdentityWithMongodb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityWithMongodb.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public AuthController(UserManager<ApplicationUser> userManager, 
                          RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var appUser = new ApplicationUser { UserName = user.Name, Email = user.Email };
        var result = await _userManager.CreateAsync(appUser, user.Password);

        if (result.Succeeded) return Ok(new { message = "User Created Successfully" });

        return BadRequest(result.Errors);
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateRole([FromBody][Required] string name)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _roleManager.CreateAsync(new ApplicationRole { Name = name });

        if (result.Succeeded) return Ok(new { message = "Role Created Successfully" });

        return BadRequest(result.Errors);
    }
}
