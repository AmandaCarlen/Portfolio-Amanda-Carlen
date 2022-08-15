using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education_API.ViewModels.Teachers;

namespace Education_API.Interfaces
{
    public interface ITeacherRepository
    {
         public Task<List<TeacherViewModel>> ListAllTeachersAsync();
        public Task<TeacherViewModel?> GetTeacherAsync(int id);
        public Task AddTeacherAsync(PostTeacherViewModel model);
        public Task DeleteTeacherAsync(int id);
        public Task UpdateTeacherAsync(int id, PatchTeacherViewModel model);
        public Task<bool> SaveAllAsync();
        public Task<string> GetUserIdAsync(int id);
        public Task<List<string>> ListTeacherSkillNamesAsync(int Id);
        public Task<List<TeacherViewModel>> ListAllTeachersWithSkillAsync(string inputSkill);



    }
}