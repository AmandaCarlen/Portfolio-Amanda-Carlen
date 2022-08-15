using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education_API.Data;
using Education_API.Interfaces;
using Education_API.Models;
using Education_API.ViewModels;
using Education_API.ViewModels.Skills;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Education_API.Controllers
{
  [ApiController]
  [Route("api/v1/skills")]
  public class SkillController : ControllerBase
  {
    private readonly ISkillRepository _skillRepo;
    public SkillController(ISkillRepository skillRepo)
    {
      _skillRepo = skillRepo;
    }

    [HttpGet()]
    public async Task<ActionResult<List<Skill>>> ListSkills()
    {
      var list = await _skillRepo.ListAllSkillsAsync();
      return Ok(list);
    }

    [HttpGet("list/names")]
    public async Task<ActionResult<List<string>>> ListAllSkillNamesAsync()
    {
      try{
        return Ok(await _skillRepo.ListAllSkillNamesAsync());
      }
      catch(Exception ex){
        return StatusCode(500, ex.Message);
      }
      
    }

    

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSkillById(int id)
    {
      return Ok(await _skillRepo.GetSkillByIdAsync(id));
    }

    [HttpPost()]
    public async Task<ActionResult> AddSkill(PostSkillViewModel model)
    {
      try
      {
        if (await _skillRepo.GetSkillByNameAsync(model.Name!) != null)
        {
          var error = new ErrorViewModel
          {
            StatusCode = 400,
            StatusText = $"Skill med namnet {model.Name} finns redan i systemet"
          };

          return BadRequest(error);
        }
        await _skillRepo.AddSkillAsync(model);

        if (await _skillRepo.SaveAllAsync())
        {
          return StatusCode(201);
        }

        return StatusCode(500, "Det gick fel när vi skulle spara skill");
      }
      catch (Exception ex)
      {

        return StatusCode(500, ex.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateSkill(int id, PostSkillViewModel model)
    {
      try
      {
        if (await _skillRepo.GetSkillByNameAsync(model.Name!) != null)
        {
          var error = new ErrorViewModel
          {
            StatusCode = 400,
            StatusText = $"Skill med namnet {model.Name} finns redan i systemet"
          };

          return BadRequest(error);
        }
        await _skillRepo.UpdateSkillAsync(id, model);

        if (await _skillRepo.SaveAllAsync())
        {
          return NoContent();
        }
        return StatusCode(500, $"Något gick fel och det gick inte att uppdatera skill {model.Name}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSkill(int id)
    {
      try
      {
        await _skillRepo.DeleteSkillAsync(id);

        if (await _skillRepo.SaveAllAsync())
        {
          return NoContent();
        }
        return StatusCode(500, $"Det gick inte att ta bort skill med id {id}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

  }

}