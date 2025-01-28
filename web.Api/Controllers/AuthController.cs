using AutoMapper;
using Core.Application.DTOs.DTOsRequests.Identity;
using Core.Application.Interfaces.Identity;
using Core.Domain.Entities;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Internal;
using System.Text.Encodings.Web;
using System.Text;

namespace web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public UsersController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO userDetails)
        {
            // 1. Validate DTO model
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid registration data", Errors = ModelState.Values.SelectMany(v => v.Errors) });
            }

            var user = _mapper.Map<User>(userDetails);

            // Call the service to register the user
            var result = await _authService.RegisterUserAsync(user, userDetails.Password);


            // Return a bad request with the errors
            if (!result.Succeeded)
            {
                // Map IdentityError to a list of errors and add them to the ModelState
                var errors = result.Errors.Select(error => error.Description).ToList();

                // Return a bad request with the errors
                return BadRequest(new { Message = "User Registration Failed", Errors = errors });
            }


            return Ok(new { Message = "User Registration Successful" });
        }



       [HttpPost("Login")]
         public async Task<IActionResult> Login([FromBody] LoginUserDto loginDetails)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(new { Message = "Invalid login data", Errors = ModelState.Values.SelectMany(v => v.Errors) });
             }
             var user = _mapper.Map<User>(loginDetails);

             // Call the login service
             var loginResult = await _authService.LoginUserAsync(user, loginDetails.Password);

             // Handle login failure
             if (!loginResult.Result.Succeeded)
             {
                 var errors = loginResult.Result.Errors.Select(error => new { error.Code, error.Description }).ToList();
                 return BadRequest(new { Message = "Login failed", Errors = errors });
             }

             // Return success with user ID and other details
             return Ok(new
             {
                 Message = "Login successful",
                token = loginResult.token

             });
        }



        /* 

         [HttpGet]
         [Route("FacebookLogin")]
         public IActionResult FacebookLogin()
         {
             var provider = "Facebook";
             var redirectUrl = Url.Action(nameof(CallBack));
             var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
             return new ChallengeResult(provider, properties);
         }

         /// <summary>
         /// Handles the callback from Facebook after login.
         /// </summary>
         [HttpGet]
         [Route("CallBack")]
         public async Task<IActionResult> CallBack(string remoteError = null)
         {
             if (remoteError != null)
             {
                 _logger.LogWarning($"External authentication failed: {remoteError}");
                 return BadRequest(new { Message = "External authentication failed.", Error = remoteError });
             }

             var info = await _signInManager.GetExternalLoginInfoAsync();
             if (info == null)
             {
                 _logger.LogWarning("External login information is null.");
                 return BadRequest(new { Message = "Error loading external login information." });
             }

             // Check if the user already exists in the system
             var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
             if (result.Succeeded)
             {
                 _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                 return Ok(new { Message = "Login successful." });
             }

             if (result.IsLockedOut)
             {
                 _logger.LogWarning("User is locked out.");
                 return Forbid();
             }

             // User doesn't exist, create a new one
             var email = info.Principal.FindFirstValue(System.Security.Claims.ClaimTypes.Email);
             var userName = email ?? info.Principal.Identity.Name;

             if (email == null)
             {
                 _logger.LogWarning("No email claim found.");
                 return BadRequest(new { Message = "Email claim not found. External login cannot proceed." });
             }

             var user = new IdentityUser { UserName = userName, Email = email };
             var createResult = await _userManager.CreateAsync(user);

             if (!createResult.Succeeded)
             {
                 _logger.LogError("User creation failed.");
                 return BadRequest(new { Message = "User creation failed.", Errors = createResult.Errors });
             }

             // Link the external login to the user
             var addLoginResult = await _userManager.AddLoginAsync(user, info);
             if (!addLoginResult.Succeeded)
             {
                 _logger.LogError("Adding external login failed.");
                 return BadRequest(new { Message = "Adding external login failed.", Errors = addLoginResult.Errors });
             }

             // Sign in the user
             await _signInManager.SignInAsync(user, isPersistent: false);

             // Send confirmation email
             var userId = await _userManager.GetUserIdAsync(user);
             var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
             code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
             var callbackUrl = Url.Page(
                 "/Account/ConfirmEmail",
                 pageHandler: null,
                 values: new { area = "Identity", userId = userId, code = code },
                 protocol: Request.Scheme);

             await _emailSender.SendEmailAsync(email, "Confirm your email",
                 $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

             _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);

             return Ok(new
             {
                 Message = "User registered successfully. Please confirm your email.",
                 UserId = userId
             });*/
    }
}
