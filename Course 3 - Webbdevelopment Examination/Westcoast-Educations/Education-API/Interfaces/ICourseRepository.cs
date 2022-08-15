using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education_API.Models;
using Education_API.ViewModels.Courses;

namespace Education_API.Interfaces
{
    public interface ICourseRepository
    {
        public Task<List<CourseViewModel>> ListAllCoursesAsync();
        public Task<CourseViewModel?> GetCourseAsync(int id);
        public Task AddCourseAsync(PostCourseViewModel model);
        public Task<List<CourseViewModel>> ListAllCoursesByCategoryAsync(string category);
        public Task DeleteCourseAsync(int id);
        public Task UpdateCourseAsync(int id, PutCourseViewModel model);
        public Task<bool> SaveAllAsync();
        public Task<List<string>> ListAllCourseNamesAsync();
        public Task<List<string>> ListTeacherCourseNamesAsync(int Id);
        public Task<List<string>> ListStudentCourseNamesAsync(int Id);




    }
}