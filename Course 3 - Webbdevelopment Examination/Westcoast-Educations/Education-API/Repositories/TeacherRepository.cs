using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Education_API.Data;
using Education_API.Interfaces;
using Education_API.Models;
using Education_API.ViewModels.Teachers;
using Microsoft.EntityFrameworkCore;

namespace Education_API.Properties
{
  public class TeacherRepository : ITeacherRepository
  {

    private readonly EducationContext _context;
    private readonly IMapper _mapper;
    public TeacherRepository(EducationContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }
    public async Task AddTeacherAsync(PostTeacherViewModel model)
    {
      var entity = await _context.Teachers.Where(e => e.IdentityUser!.Email!.ToLower() == model.Email!.ToLower()).FirstOrDefaultAsync();
      if(entity != null)
      {
        throw new Exception($"Det finns redan en lärare med email: {model.Email}");
      }
      var identityUser = await _context.Users.Where(i => i.Email!.ToLower() == model.Email!.ToLower()).FirstOrDefaultAsync();
      // ICollection<Course> courseList = new List<Course>();
      // ICollection<Skill> skillList = new List<Skill>();
      List<Skill> skillList = new List<Skill>();

      if (model.Skills.Count > 0)
      {
        foreach (var skillName in model.Skills)
        {

          var skill = await _context.Skills.Where(s => s.Name.ToLower() == skillName.ToLower()).FirstOrDefaultAsync();
          if (skill == null)
          {
            throw new Exception($"Kunde inte hitta någon skill med namnet: {skillName} i vårt system");
          }
          else
          {
            skillList.Add(skill);
          }
        }
      }

      var teacher = _mapper.Map<Teacher>(model);
      teacher.Skill = skillList;
      teacher.IdentityUser = identityUser!;
      teacher.IdentityUserId = identityUser!.Id;
      await _context.Teachers.AddAsync(teacher);
    }

    public async Task DeleteTeacherAsync(int id)
    {
      var teacher = await _context.Teachers.FindAsync(id);

      if (teacher is null) throw new Exception($"Kunde inte hitta lärare med id: {id}");

      _context.Teachers.Remove(teacher);
    }

    public async Task<TeacherViewModel?> GetTeacherAsync(int id)
    {
      var teacher = await _context.Teachers.Where(s => s.Id == id).ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
      // return await _context.Teachers.Where(s => s.Id == id).ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
      return teacher;
    }

     public async Task<List<string>> ListTeacherSkillNamesAsync(int Id)
    {
      var teacher = await _context.Teachers.Include(c => c.Skill).Where(t => t.Id == Id).FirstOrDefaultAsync();
      var response = new List<string>();
      if(teacher== null)
      {return response;}

      foreach(var skill in teacher.Skill)
      {
        response.Add(skill.Name);
      }
      return response;
    }

    public async Task<string> GetUserIdAsync(int id)
    {
      var teacher = await _context.Teachers.FindAsync(id);
      if(teacher == null)
      {
        throw new Exception($"Det finns ingen lärare med id {id}");
      }
      return teacher!.IdentityUserId!.ToString();
    }

    public async Task<List<TeacherViewModel>> ListAllTeachersAsync()
    {
      return await _context.Teachers.ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public async Task UpdateTeacherAsync(int id, PatchTeacherViewModel model)
    {

      var teacher = await _context.Teachers.Include(t => t.Course).Include(t => t.Skill).SingleOrDefaultAsync(t => t.Id == id);

      if (teacher is null)
        {throw new Exception($"Kunde inte hitta någon lärare med namnet id: {id} i vårt system");}

      ICollection<Course> courseList = new List<Course>();
      ICollection<Skill> skillList = new List<Skill>();

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

      if (model.Skills.Count() > 0)
      {
        foreach (var s in model.Skills)
        {

          var skill = await _context.Skills.Where(x => x.Name.ToLower() == s.ToLower()).FirstOrDefaultAsync();

          if (skill == null)
          {
            throw new Exception($"Kunde inte hitta någon skill med namnet: {s} i vårt system");
          }
          else
          {
            skillList.Add(skill);
          }
        }
      }

      teacher.Course = courseList;
      teacher.Skill = skillList;
       _mapper.Map<PatchTeacherViewModel, Teacher>(model, teacher);

      _context.Teachers.Update(teacher);
    }

    public async Task<List<TeacherViewModel>> ListAllTeachersWithSkillAsync(string inputSkill)
    {
      var allTeachers = await _context.Teachers.ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider).ToListAsync();
      // var allTeachers = await _context.Teachers.Include(t => t.Skill).ToListAsync();
      List<TeacherViewModel> response = new List<TeacherViewModel>();

      foreach(var teacher in allTeachers)
      {
        foreach(var skill in teacher.Skill)
        {
          if(skill.Name == inputSkill)
          {
            response.Add(teacher);
          }
        }
      }

      return response;
      // return await _context.Teachers.Include(c => c.Skill)
      // .Where(c => c.Skill.Name!.ToLower() == skill.ToLower())
      // .ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider)
      // .ToListAsync();
    }
  }
}