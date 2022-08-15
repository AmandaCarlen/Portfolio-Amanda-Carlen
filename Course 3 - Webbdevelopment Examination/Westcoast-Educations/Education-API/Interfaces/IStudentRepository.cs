using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education_API.ViewModels.Courses;
using Education_API.ViewModels.Students;

namespace Education_API.Interfaces
{
    public interface IStudentRepository
    {
         public Task<List<StudentViewModel>> ListAllStudentsAsync();
        public Task<StudentViewModel?> GetStudentAsync(int id);
        public Task AddStudentAsync(PostStudentViewModel model);
        public Task DeleteStudentAsync(int id);
        public Task UpdateStudentAsync(int id, PatchStudentViewModel model);
        public Task<bool> SaveAllAsync();
        public Task<string> GetUserIdAsync(int id);

    }
}