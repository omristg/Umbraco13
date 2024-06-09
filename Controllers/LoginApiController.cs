using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.Security;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;

namespace Umbraco13Test.Controllers;

public class LoginApiController : UmbracoApiController
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

    [HttpPost]
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
            return Ok();
        }
        return Unauthorized("Wrong email or password");
    }

    [HttpGet]
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

    public IActionResult Logout()
    {
        _signInManager.SignOutAsync().GetAwaiter();
        return Ok();
    }

}

public class LoginModel
{
    public string? email { get; set; }
    public string? password { get; set; }

}
