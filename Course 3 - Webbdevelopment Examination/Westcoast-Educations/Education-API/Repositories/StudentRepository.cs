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
using Education_API.ViewModels.Students;
using Microsoft.EntityFrameworkCore;

namespace Education_API.Properties
{
  public class StudentRepository : IStudentRepository
  {
    private readonly EducationContext _context;
    private readonly IMapper _mapper;
    public StudentRepository(EducationContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }
    public async Task AddStudentAsync(PostStudentViewModel model)
    {
      var entity = await _context.Students.Where(e => e.IdentityUser!.Email!.ToLower() == model.Email!.ToLower()).FirstOrDefaultAsync();
      if(entity != null)
      {
        throw new Exception($"Det finns redan en student med email: {model.Email}");
      }
      var identityUser = await _context.Users.Where(i => i.Email!.ToLower() == model.Email!.ToLower()).FirstOrDefaultAsync();
      // ICollection<Course> list = new List<Course>();

      // if (model.Courses != null)
      // {
      //   foreach (var c in model.Courses)
      //   {

      //     var course = await _context.Courses.FindAsync(c);
      //     if (course == null)
      //     {
      //       throw new Exception($"Kunde inte hitta någon kurs med id: {c} i vårt system");
      //     }
      //     else
      //     {
      //       list.Add(course);
      //     }
      //   }
      // }

      var student = _mapper.Map<Student>(model);
      // student.Course = list;
      student.IdentityUser = identityUser!;
      student.IdentityUserId = identityUser!.Id;

      await _context.Students.AddAsync(student);
    }

    public async Task DeleteStudentAsync(int id)
    {
      var student = await _context.Students.FindAsync(id);

      if (student is null) throw new Exception($"Kunde inte hitta lärare med id: {id}");

      _context.Students.Remove(student);
    }

    public async Task<StudentViewModel?> GetStudentAsync(int id)
    {      
      var student = await _context.Students.Where(s => s.Id == id).ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
      // return await _context.Students.Where(s => s.Id == id).ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
      return student;
    }

    public async Task<List<StudentViewModel>> ListAllStudentsAsync()
    {
      return await _context.Students.ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public async Task<string> GetUserIdAsync(int id)
    {
      var student = await _context.Students.FindAsync(id);
      if(student == null)
      {
        throw new Exception($"Det finns ingen student med id {id}");
      }
      return student!.IdentityUserId!.ToString();
    }

    public async Task UpdateStudentAsync(int id, PatchStudentViewModel model)
    {
      var student = await _context.Students.Include(s => s.Course).SingleOrDefaultAsync(s => s.Id == id);

      if (student is null)
        throw new Exception($"Kunde inte hitta någon student med namnet id: {id} i vårt system");

      ICollection<Course> courseList = new List<Course>();

      if (model.Courses.Count() > 0)
      {
        foreach (var c in model.Courses)
        {
          var course = await _context.Courses.Where(x => x.Title.ToLower() == c.ToLower()).FirstOrDefaultAsync();
          if (course == null)
          {
            throw new Exception($"Kunde inte hitta någon kurs med namet: {c} i vårt system");
          }
          else
          {
            courseList.Add(course);
          }
        }
      }

      student.Course = courseList;

      _mapper.Map<PatchStudentViewModel, Student>(model, student);

      _context.Students.Update(student);
    }
  }
}