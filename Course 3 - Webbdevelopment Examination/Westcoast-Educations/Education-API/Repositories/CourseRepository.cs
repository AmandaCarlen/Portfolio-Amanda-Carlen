using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Education_API.Data;
using Education_API.Interfaces;
using Education_API.Models;
using Education_API.ViewModels.Courses;
using Microsoft.EntityFrameworkCore;

namespace Education_API.Properties
{

  public class CourseRepository : ICourseRepository
  {
    private readonly EducationContext _context;
    private readonly IMapper _mapper;
    public CourseRepository(EducationContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task AddCourseAsync(PostCourseViewModel model)
    {
        var category = await _context.Categories.Include(c => c.Courses).Where(c => c.Name!.ToLower() == model.Category!.ToLower()).SingleOrDefaultAsync();

        // var courseToAdd = _mapper.Map<Course>(model);
        var courseToAdd = new Course();

        if(category == null)
        {
            Category newCategory = new Category();
            newCategory.Name = model.Category;
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            courseToAdd.Category= newCategory;
        }
        else{
        courseToAdd.Category = category;
        }

        courseToAdd.Id = model.Id;
        courseToAdd.Title = model.Title;
        courseToAdd.Length = model.Length;
        courseToAdd.Description = model.Description;
        courseToAdd.Details = model.Details;

        await _context.Courses.AddAsync(courseToAdd);
    }

    public async Task DeleteCourseAsync(int id)
    {
      var response = await _context.Courses.FindAsync(id);

      if (response is null)
      {
        throw new Exception($"Vi kunde inte hitta en kurs med kursnummer: {id}");
      }

      if (response is not null)
      {
        _context.Courses.Remove(response);
      }

    }
    
    public async Task<CourseViewModel?> GetCourseAsync(int courseId)
    {
        return await _context.Courses
        .Where(c => c.Id == courseId)
        .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task<List<string>> ListAllCourseNamesAsync()
    {
        return await _context.Courses.ProjectTo<string>(_mapper.ConfigurationProvider).ToListAsync();

    }

    public async Task<List<string>> ListTeacherCourseNamesAsync(int Id)
    {
      var teacher = await _context.Teachers.Include(c => c.Course).Where(t => t.Id == Id).FirstOrDefaultAsync();
      var response = new List<string>();
      if(teacher== null)
      {return response;}

      foreach(var course in teacher.Course)
      {
        response.Add(course.Title);
      }
      return response;

    }
     public async Task<List<string>> ListStudentCourseNamesAsync(int Id)
    {
      var student = await _context.Students.Include(c => c.Course).Where(t => t.Id == Id).FirstOrDefaultAsync();
      var response = new List<string>();
      if(student== null)
      {return response;}

      foreach(var course in student.Course)
      {
        response.Add(course.Title);
      }
      return response;

    }

    public async Task<List<CourseViewModel>> ListAllCoursesAsync()
    {
      return await _context.Courses
      .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider)
      .ToListAsync();
    }

    public async Task<List<CourseViewModel>> ListAllCoursesByCategoryAsync(string name)
    {       
      return await _context.Courses.Include(c => c.Category)
      .Where(c => c.Category.Name!.ToLower() == name.ToLower())
      .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider)
      .ToListAsync();
    }

    public async Task<List<string>> ListAllStudentCourseNamesAsync(int Id)
    {
      var student = await _context.Students.Include(c => c.Course).Where(t => t.Id == Id).FirstOrDefaultAsync();
      var response = new List<string>();
      if(student== null)
      {return response;}

      foreach(var course in student.Course)
      {
        response.Add(course.Title);
      }
      return response;
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;

    }

    public async Task UpdateCourseAsync(int id, PutCourseViewModel model)
    {
      var course = await _context.Courses.FindAsync(id);

      if(course is null)
      {
        throw new Exception($"Vi kunde inte hitta någon kurs med kursnummer: {id}");
      }

      var category = await _context.Categories.Include(c => c.Courses).Where(c => c.Name!.ToLower() == model.Category!.ToLower()).SingleOrDefaultAsync();


      if(category == null)
        {
            Category newCategory = new Category();
            newCategory.Name = model.Category;
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            course.Category= newCategory;
        }
        else{
            course.Category = category;

        }

      course.Title = model.Title;
      course.Length = model.Length;
      course.Description = model.Description;
      course.Details = model.Details;

      _context.Courses.Update(course);
    }
  }
}