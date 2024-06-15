using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Security;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco13Test.Models;
using System.ComponentModel.DataAnnotations;
using Umbraco13Test.Services;
using System.Web;

namespace Umbraco13Test.Controllers;

[Route("api/[controller]")]
[ApiController]
[EndpointGroupName("Authentication")]
public class LoginApiController : ControllerBase
{
    private readonly IMemberSignInManager _signInManager;
    private readonly IMemberManager _memberManager;
    private readonly IMemberService _memberService;
    private readonly IMailService _mailService;


    public LoginApiController(IMemberSignInManager signInManager, IMemberManager memberManager, IMemberService memberService, IMailService mailService)
    {
        _signInManager = signInManager;
        _memberManager = memberManager;
        _memberService = memberService;
        _mailService = mailService;
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
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }


    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = await _memberManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return BadRequest("User not found.");
        }

        var token = await _memberManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = HttpUtility.UrlEncode(token);
        var resetLink = $"http://localhost:3000/auth/reset-password?email={model.Email}&token={encodedToken}";

        await _mailService.SendAsync(model.Email, "Reset Password", $"Please reset your password by clicking here: {resetLink}");

        return Ok("Password reset link has been sent to your email.");
    }


    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _memberManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return BadRequest("User not found.");
        }

        var result = await _memberManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
        if (result.Succeeded)
        {
            return Ok("Password has been reset successfully.");
        }
        return BadRequest("Failed to reset password.");
    }
}



public class ForgotPasswordModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}

public class ResetPasswordModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Token { get; set; } = string.Empty;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    public string NewPassword { get; set; } = string.Empty;
}
