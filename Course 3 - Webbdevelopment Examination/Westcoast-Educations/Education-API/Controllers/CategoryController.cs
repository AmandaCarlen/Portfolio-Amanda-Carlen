using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Education_API.Data;
using Education_API.Interfaces;
using Education_API.Models;
using Education_API.ViewModels;
using Education_API.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Education_API.Controllers
{
  [ApiController]
  [Route("api/v1/categories")]
  public class CategoryController : ControllerBase
  {
    private readonly ICategoryRepository _categoryRepo;
    public CategoryController(ICategoryRepository categoryRepo)
    {
      _categoryRepo = categoryRepo;
    }

    [HttpGet()]
    public async Task<ActionResult<List<CategoryViewModel>>> ListCategories()
    {
      var list = await _categoryRepo.ListAllCategoriesAsync();
      return Ok(list);
    }

    [HttpGet("list/names")]
    public async Task<ActionResult<List<string>>> ListCategoryNames()
    {
      var list = await _categoryRepo.ListAllCategoryNamesAsync();
      return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCategoryById(int id)
    {
      return Ok(await _categoryRepo.GetCategoryByIdAsync(id));
    }

    [HttpPost()]
    public async Task<ActionResult> AddCategory(PostCategoryViewModel model)
    {
      try
      {
        if (await _categoryRepo.GetCategoryByNameAsync(model.Name!) != null)
        {
          var error = new ErrorViewModel
          {
            StatusCode = 400,
            StatusText = $"Kategorin med namnet {model.Name} finns redan i systemet"
          };

          return BadRequest(error);
        }
        await _categoryRepo.AddCategoryAsync(model);

        if (await _categoryRepo.SaveAllAsync())
        {
          return StatusCode(201);
        }

        return StatusCode(500, "Det gick fel när vi skulle spara kategorin");
      }
      catch (Exception ex)
      {

        return StatusCode(500, ex.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCategory(int id, PostCategoryViewModel model)
    {
      try
      {
        if (await _categoryRepo.GetCategoryByNameAsync(model.Name!) != null)
        {
          var error = new ErrorViewModel
          {
            StatusCode = 400,
            StatusText = $"Kategorin med namnet {model.Name} finns redan i systemet"
          };

          return BadRequest(error);
        }
        await _categoryRepo.UpdateCategoryAsync(id, model);

        if (await _categoryRepo.SaveAllAsync())
        {
          return NoContent();
        }
        return StatusCode(500, $"Något gick fel och det gick inte att uppdatera kategorin {model.Name}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
      try
      {
        await _categoryRepo.DeleteCategoryAsync(id);

        if (await _categoryRepo.SaveAllAsync())
        {
          return NoContent();
        }
        return StatusCode(500, $"Det gick inte att ta bort kategorin med id {id}");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

  }

}