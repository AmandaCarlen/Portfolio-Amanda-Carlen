using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education_API.ViewModels.Skills;

namespace Education_API.Interfaces
{
    public interface ISkillRepository
    {
         public Task<SkillViewModel> GetSkillByNameAsync(string name);
        public Task<SkillViewModel> GetSkillByIdAsync(int id);
        public Task<List<SkillViewModel>> ListAllSkillsAsync();
        public Task<List<string>> ListAllSkillNamesAsync();


        public Task AddSkillAsync(PostSkillViewModel model);
        public Task DeleteSkillAsync(int id);
        public Task UpdateSkillAsync(int id, PostSkillViewModel model);
        public Task<bool> SaveAllAsync();
    }
}