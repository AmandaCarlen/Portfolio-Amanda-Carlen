using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Education_API.Data;
using Education_API.Interfaces;
using Education_API.ViewModels.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Education_API.Repositories
{
    public class AuthRepository : IAuthRepository
    {

    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signManager;
    private readonly IConfiguration _config;
    private readonly EducationContext _context;

    public AuthRepository(IConfiguration config, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, EducationContext context)
    {
      _context = context;
      _roleManager = roleManager;
      _userManager = userManager;
      _signManager = signInManager;
      _config = config;
    }

    public async Task<ActionResult<UserViewModel>> RegisterUserAsync(RegisterUserViewModel model)
    {
        // Lägg till för att se om användaren redan finns
        // var entity = await _context.Users.FindAsync(u => u.Email!.ToLower() == model.Email!.ToLower());
      var user = new IdentityUser
      {
        Email = model.Email!.ToLower(),
        UserName = model.Email.ToLower(),
        PhoneNumber = model.PhoneNumber
      };

      var result = await _userManager.CreateAsync(user, model.Password);

      if (result.Succeeded)
      {
        if (model.IsAdmin)
        {
          await _userManager.AddClaimAsync(user, new Claim("Admin", "true"));
        }

        if(model.IsTeacher)
        {
          await _userManager.AddClaimAsync(user, new Claim("Teacher", "true"));
        }

        if(model.IsStudent)
        {
          await _userManager.AddClaimAsync(user, new Claim("Student", "true"));
        }

        await _userManager.AddClaimAsync(user, new Claim("User", "true"));
        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.UserName));
        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));

        var userData = new UserViewModel
        {
          UserName = user.UserName,
          Token = await CreateJwtTokenAsync(user)
        };

        return userData;

      }
      else
      {
        throw new Exception($"Could not create user");
        // foreach (var error in result.Errors)
        // {
        //   ModelState.AddModelError("User registration", error.Description);
        // }
        // return StatusCode(500, ModelState);
      }
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;

    }

    public async Task<string> CreateJwtTokenAsync(IdentityUser user)
    {
      var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("apiKey"));
      var userClaims = (await _userManager.GetClaimsAsync(user)).ToList();

      var jwt = new JwtSecurityToken(
          claims: userClaims,
          notBefore: DateTime.Now,
          expires: DateTime.Now.AddDays(7),
          signingCredentials: new SigningCredentials(
              new SymmetricSecurityKey(key),
              SecurityAlgorithms.HmacSha512Signature
          )
      );
      return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public async Task DeleteUserAsync(string email)
    {
       var user = await _userManager.FindByEmailAsync(email); 

      if (user is null) throw new Exception($"Kunde inte hitta user med email: {email}");

      _context.Users.Remove(user);
    }
    public async Task DeleteStudentUserAsync(int id)
    {
      var student = await _context.Students.FindAsync(id);
      if(student == null)
      {
        throw new Exception($"Kunde inte hitta en användare med id: {id}");
      }
      var user = await _context.Users.FindAsync(student.IdentityUserId);

      // var user = await _context.Users.FindAsync(student.IdentityUser!.Email);

      if (user is null) throw new Exception($"Kunde inte hitta user med email: {student.IdentityUser!.Email}");

      _context.Users.Remove(user);
    }
     public async Task DeleteTeacherUserAsync(int id)
    {
      var teacher = await _context.Teachers.FindAsync(id);
      if(teacher == null)
      {
        throw new Exception($"Kunde inte hitta en användare med id: {id}");
      }
      var user = await _context.Users.FindAsync(teacher.IdentityUserId);

      // var user = await _context.Users.FindAsync(student.IdentityUser!.Email);

      if (user is null) throw new Exception($"Kunde inte hitta user med email: {teacher.IdentityUser!.Email}");

      _context.Users.Remove(user);
    }

    public async Task UpdateUserAsync(string id, PatchUserViewModel model)
    {
      var user = await _userManager.FindByIdAsync(id);
      user.PhoneNumber = model.PhoneNumber;

      _context.Users.Update(user);     
    }
  }
}