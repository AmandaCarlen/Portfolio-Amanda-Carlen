using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Education_API.Data;
using Education_API.Interfaces;
using Education_API.Models;
using Education_API.ViewModels.Categories;
using Microsoft.EntityFrameworkCore;

namespace Education_API.Properties
{
  public class CategoryRepository : ICategoryRepository
  {

    private readonly EducationContext _context;
    private readonly IMapper _mapper;
    public CategoryRepository(EducationContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task AddCategoryAsync(PostCategoryViewModel model)
    {
      var category = _mapper.Map<Category>(model);
      await _context.Categories.AddAsync(category);
    }

    public async Task DeleteCategoryAsync(int id)
    {
      var result = await _context.Categories.FindAsync(id);

      if (result == null) 
      throw new Exception($"Kunde inte hitta kategori med id: {id}");

      _context.Categories.Remove(result);
    }

    public async Task<CategoryViewModel> GetCategoryByIdAsync(int id)
    {
      return _mapper.Map<CategoryViewModel>(await _context.Categories
      .FindAsync(id));
    }

    public async Task<CategoryViewModel> GetCategoryByNameAsync(string name)
    {
      return _mapper.Map<CategoryViewModel>(await _context.Categories
      .Where(c => c.Name!.ToLower() == name.ToLower())
      .SingleOrDefaultAsync());
    }

    public async Task<List<CategoryViewModel>> ListAllCategoriesAsync()
    {
      return await _context.Categories.ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider).ToListAsync();

    }

    public async Task<List<string>> ListAllCategoryNamesAsync()
    {
        return await _context.Categories.ProjectTo<string>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public async Task UpdateCategoryAsync(int id, PostCategoryViewModel model)
    {
      var category = await _context.Categories.FindAsync(id);

      if (category is null) 
      {
        throw new Exception($"Kunde inte hitta kategori med id: {id}");
      }
      else{
        category.Name = model.Name;
      }

      _context.Categories.Update(category);


    }

  }
}