using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Education_API.Data;
using Education_API.Interfaces;
using Education_API.Models;
using Education_API.ViewModels.Authorization;
using Education_API.ViewModels.Students;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Education_API.Controllers
{
    [ApiController]
  [Route("api/v1/students")]
  public class StudentController : ControllerBase
  {
    private readonly IStudentRepository _studentRepo;
    private readonly IAuthRepository _authRepo;
    private readonly IMapper _mapper;
    public StudentController(IStudentRepository studentRepo, IAuthRepository authRepo, IMapper mapper)
    {
      _mapper = mapper;
      _authRepo = authRepo;
      _studentRepo = studentRepo;
    }

    [HttpGet()]
    public async Task<ActionResult<List<Student>>> ListStudents()
    {
      var list = await _studentRepo.ListAllStudentsAsync();
      return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetStudentById(int id)
    {
      return Ok(await _studentRepo.GetStudentAsync(id));
    }

    [HttpPost()]
    public async Task<ActionResult> AddStudent(PostStudentViewModel model)
    {
      try
      {
        await _authRepo.RegisterUserAsync(_mapper.Map<RegisterUserViewModel>(model));
      await _studentRepo.AddStudentAsync(model);

        if (await _studentRepo.SaveAllAsync())
        {
          return StatusCode(201);
        }

        return StatusCode(500, "Det gick fel när vi skulle spara studenten");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateStudent(int id, PatchStudentViewModel model)
    {
      try
      {
        var userId = await _studentRepo.GetUserIdAsync(id);
        if(model.PhoneNumber == null)
        {
          return BadRequest("Telefon nummer måste finnas");
        }
        await _authRepo.UpdateUserAsync(userId, _mapper.Map<PatchUserViewModel>(model));

        await _studentRepo.UpdateStudentAsync(id, model);

        if (await _studentRepo.SaveAllAsync())
        {
          return NoContent();
        }
        return StatusCode(500, $"Något gick fel och det gick inte att uppdatera student med id: {id}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStudent(int id)
    {
      try
      {
        await _studentRepo.DeleteStudentAsync(id);
        await _authRepo.DeleteStudentUserAsync(id);

        if (await _studentRepo.SaveAllAsync())
        {
          return NoContent();
        }

        return StatusCode(500, $"Det gick inte att ta bort student med id: {id}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
  }
}