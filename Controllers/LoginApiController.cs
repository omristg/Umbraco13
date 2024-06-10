using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Security;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco13Test.Models;

namespace Umbraco13Test.Controllers;

[Route("api/[controller]")]
[ApiController]
[EndpointGroupName("Authentication")]
public class LoginApiController : ControllerBase
{
    private readonly IMemberSignInManager _signInManager;
    private readonly IMemberManager _memberManager;
    private readonly IMemberService _memberService;


    public LoginApiController(IMemberSignInManager signInManager, IMemberManager memberManager, IMemberService memberService)
    {
        _signInManager = signInManager;
        _memberManager = memberManager;
        _memberService = memberService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        if (loginModel is null || loginModel.email is null || loginModel.password is null)
        {
            return BadRequest("Email and password must be provided");
        }
        var member = _memberService.GetByEmail(loginModel.email);
        if (member is null)
        {
            return Unauthorized("Wrong email or password");
        }
        var result = await _signInManager.PasswordSignInAsync(member.Username, loginModel.password, false, true);
        if (result.Succeeded)
        {
            return Ok("Logged in");
        }
        return Unauthorized("Wrong email or password");
    }

    [HttpGet("CheckLoggedInMember")]
    public IActionResult CheckedLoggedInMember()
    {
        var isLoggedIn = _memberManager.IsLoggedIn();
        var member = _memberManager.GetCurrentMemberAsync().GetAwaiter().GetResult();
        if (isLoggedIn)
        {
            return Ok("Logged in, Member name is: " + member?.Name);
        }
        else
        {
            return BadRequest("Not logged in");
        }

    }

    [HttpPost("Logout")]
    public IActionResult Logout()
    {
        _signInManager.SignOutAsync().GetAwaiter();
        return Ok();
    }

}


