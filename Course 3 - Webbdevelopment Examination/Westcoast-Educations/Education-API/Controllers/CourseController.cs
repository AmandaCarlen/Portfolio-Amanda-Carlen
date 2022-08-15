using Education_API.Data;
using Education_API.Interfaces;
using Education_API.Models;
using Education_API.ViewModels;
using Education_API.ViewModels.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Education_API.Controllers
{
  [ApiController]
  [Route("api/v1/courses")]
  public class CourseController : ControllerBase
  {

    private readonly ICourseRepository _courseRepo;
    public CourseController(ICourseRepository courseRepo)
    {
      _courseRepo = courseRepo;
    }

    [HttpGet()]
    public async Task<ActionResult<List<Course>>> ListCourses()
    {
      return Ok(await _courseRepo.ListAllCoursesAsync());
    }

    [HttpGet("list/names")]
    public async Task<ActionResult<List<string>>> ListAllSkillNamesAsync()
    {
      try{
        return Ok(await _courseRepo.ListAllCourseNamesAsync());
      }
      catch(Exception ex){
        return StatusCode(500, ex.Message);
      }
      
    }
    [HttpGet("list/TeacherCourse/{id}")]
    public async Task<ActionResult<List<string>>> ListTeacherCourseNamesAsync(int Id)
    {
      try{
        return Ok(await _courseRepo.ListTeacherCourseNamesAsync(Id));
      }
      catch(Exception ex){
        return StatusCode(500, ex.Message);
      }
      
    }
    [HttpGet("list/StudentCourse/{id}")]
    public async Task<ActionResult<List<string>>> ListAllStudentCourseNamesAsync(int Id)
    {
      try{
        return Ok(await _courseRepo.ListStudentCourseNamesAsync(Id));
      }
      catch(Exception ex){
        return StatusCode(500, ex.Message);
      }
      
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseViewModel>> GetCourseById(int id)
    {
      var response = await _courseRepo.GetCourseAsync(id);

      if (response == null)
      {
        return NotFound($"Vi kunde inte hitta en kurs med kursnummer: {id}");
      }

      return Ok(response);
    }

    [HttpPost()]
    public async Task<ActionResult> AddCourse(PostCourseViewModel model)
    {

      try
      {

        if (await _courseRepo.GetCourseAsync(model.Id) != null)
        {
          var error = new ErrorViewModel
          {
            StatusCode = 400,
            StatusText = $"Kursnummret {model.Id} finns redan i systemet"
          };

          return BadRequest(error);
        }
        

        await _courseRepo.AddCourseAsync(model);

        if (await _courseRepo.SaveAllAsync())
        {
          return StatusCode(201);
        }

        return StatusCode(500, "Det gick inte att spara kursen");
      }
      catch (Exception ex)
      {
        var error = new ErrorViewModel
        {
          StatusCode = 500,
          StatusText = ex.Message
        };
        return StatusCode(500, error);
      }
    }

    [HttpGet("bycategory/{category}")]
    public async Task<ActionResult<List<CourseViewModel>>> GetCourseByCategoryAsync(string category)
    {
      return Ok(await _courseRepo.ListAllCoursesByCategoryAsync(category));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCourse(int id, PutCourseViewModel model)
    {
      try
      {
        await _courseRepo.UpdateCourseAsync(id, model);

        if (await _courseRepo.SaveAllAsync())
        {
          return NoContent();
        }

        return StatusCode(500, "Ett fel inträffade när kursen skulle uppdateras");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(int id)
    {

      try
      {
        await _courseRepo.DeleteCourseAsync(id);

        if (await _courseRepo.SaveAllAsync())
        {
          return NoContent();

        }
        return StatusCode(500, "Hoppsan något gick fel");

      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

  }
}