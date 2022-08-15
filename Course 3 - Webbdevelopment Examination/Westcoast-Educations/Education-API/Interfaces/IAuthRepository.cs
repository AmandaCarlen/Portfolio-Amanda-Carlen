using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education_API.ViewModels.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Education_API.Interfaces
{
    public interface IAuthRepository
    {
        public Task<bool> SaveAllAsync();
        public  Task<ActionResult<UserViewModel>> RegisterUserAsync(RegisterUserViewModel model);
        public Task<string> CreateJwtTokenAsync(IdentityUser user);
        public Task UpdateUserAsync(string id, PatchUserViewModel model);


        public Task DeleteUserAsync(string email);
        public Task DeleteTeacherUserAsync(int id);
        public Task DeleteStudentUserAsync(int id);




    }
}