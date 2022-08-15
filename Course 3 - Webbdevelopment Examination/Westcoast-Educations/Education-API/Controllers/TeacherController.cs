using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Education_API.Data;
using Education_API.Interfaces;
using Education_API.Models;
using Education_API.ViewModels.Authorization;
using Education_API.ViewModels.Teachers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Education_API.Controllers
{
  [ApiController]
  [Route("api/v1/teachers")]
  public class TeacherController : ControllerBase
  {
    private readonly ITeacherRepository _teacherRepo;
    private readonly IAuthRepository _authRepo;
    private readonly IMapper _mapper;
    public TeacherController(ITeacherRepository teacherRepo, IAuthRepository authRepo, IMapper mapper)
    {
      _mapper = mapper;
      _authRepo = authRepo;
      _teacherRepo = teacherRepo;
    }

    [HttpGet()]
    public async Task<ActionResult<List<Teacher>>> ListTeachers()
    {
      var list = await _teacherRepo.ListAllTeachersAsync();
      return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetTeacherById(int id)
    {
      return Ok(await _teacherRepo.GetTeacherAsync(id));
    }

   [HttpGet("byskill/{skill}")]
    public async Task<ActionResult<List<TeacherViewModel>>> GetTeachersBySkillAsync(string skill)
    {
      return Ok(await _teacherRepo.ListAllTeachersWithSkillAsync(skill));
    }
    
    [HttpGet("list/names/connected/{id}")]
    public async Task<ActionResult<List<string>>> ListAllSkillNamesAsync(int Id)
    {
      try{
        return Ok(await _teacherRepo.ListTeacherSkillNamesAsync(Id));
      }
      catch(Exception ex){
        return StatusCode(500, ex.Message);
      }
      
    }

    [HttpPost()]
    public async Task<ActionResult> AddTeacher(PostTeacherViewModel model)
    {
      try
      {
        await _authRepo.RegisterUserAsync(_mapper.Map<RegisterUserViewModel>(model));
        await _teacherRepo.AddTeacherAsync(model);

        if (await _teacherRepo.SaveAllAsync())
        {
          return StatusCode(201);
        }

        return StatusCode(500, "Det gick fel när vi skulle spara läraren");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTeacher(int id, PatchTeacherViewModel model)
    {
       try
      {
        var userId = await _teacherRepo.GetUserIdAsync(id);
        if(model.PhoneNumber == null)
        {
          return BadRequest("Telefon nummer måste finnas");
        }
        await _authRepo.UpdateUserAsync(userId, _mapper.Map<PatchUserViewModel>(model));

        await _teacherRepo.UpdateTeacherAsync(id, model);

        if (await _teacherRepo.SaveAllAsync())
        {
          return NoContent();
        }
        return StatusCode(500, $"Något gick fel och det gick inte att uppdatera läraren med id: {id}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTeacher(int id)
    {
      try
      {
        await _teacherRepo.DeleteTeacherAsync(id);
        await _authRepo.DeleteTeacherUserAsync(id);


        if (await _teacherRepo.SaveAllAsync())
        {
          return NoContent();
        }

        return StatusCode(500, $"Det gick inte att ta bort lärare med id: {id}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
  }
}