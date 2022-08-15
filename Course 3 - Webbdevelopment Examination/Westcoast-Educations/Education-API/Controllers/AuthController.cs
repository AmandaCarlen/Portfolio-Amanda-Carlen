using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Education_API.Interfaces;
using Education_API.ViewModels.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Education_API.Controllers
{
  [ApiController]
  [Route("api/v1/auth")]
  public class AuthController : ControllerBase
  {
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signManager;
    private readonly IConfiguration _config;
    private readonly IAuthRepository _repo;

    public AuthController(IConfiguration config, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IAuthRepository repo)
    {
      _repo = repo;
      _roleManager = roleManager;
      _userManager = userManager;
      _signManager = signInManager;
      _config = config;
    }

    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<UserViewModel>> Login(LoginViewModel model)
    {
      var user = await _userManager.FindByNameAsync(model.UserName);

      if (user is null)
        return Unauthorized("Felaktigt användarnamn");

      var result = await _signManager.CheckPasswordSignInAsync(user, model.Password, false);

      if (!result.Succeeded)
        return Unauthorized();

      var userData = new UserViewModel
      {
        UserName = user.UserName,
        Expires = DateTime.Now.AddDays(7),
        Token = await _repo.CreateJwtTokenAsync(user)
      };

      return Ok(userData);
    } 

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<ActionResult<UserViewModel>> RegisterUser(RegisterUserViewModel model){
      try{
        var user = await _repo.RegisterUserAsync(model);
        return StatusCode(201, user);
      }
      catch(Exception ex){
        return StatusCode(500, ex.Message);
      }
    }

    [HttpDelete("{email}")]
    public async Task<ActionResult> DeleteUser(string email){
      try
      {
        await _repo.DeleteUserAsync(email);

        if (await _repo.SaveAllAsync())
        {
          return NoContent();
        }

        return StatusCode(500, $"Det gick inte att ta bort användare med email: {email}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }  

  }
}