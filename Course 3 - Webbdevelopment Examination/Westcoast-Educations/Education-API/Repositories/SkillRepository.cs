using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Education_API.Data;
using Education_API.ViewModels.Skills;
using Education_API.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Education_API.Interfaces;


namespace Education_API.Repositories
{
    public class SkillRepository : ISkillRepository
    {
         private readonly EducationContext _context;
    private readonly IMapper _mapper;
    public SkillRepository(EducationContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task AddSkillAsync(PostSkillViewModel model)
    {
      var skill = _mapper.Map<Skill>(model);
      await _context.Skills.AddAsync(skill);
    }

    public async Task DeleteSkillAsync(int id)
    {
      var result = await _context.Skills.FindAsync(id);

      if (result == null) 
      throw new Exception($"Kunde inte hitta kategori med id: {id}");

      _context.Skills.Remove(result);
    }

    public async Task<SkillViewModel> GetSkillByIdAsync(int id)
    {
      return _mapper.Map<SkillViewModel>(await _context.Skills
      .FindAsync(id));
    }

    public async Task<SkillViewModel> GetSkillByNameAsync(string name)
    {
      return _mapper.Map<SkillViewModel>(await _context.Skills
      .Where(c => c.Name!.ToLower() == name.ToLower())
      .SingleOrDefaultAsync());
    }

   

    public async Task<List<string>> ListAllSkillNamesAsync()
    {
      var result = await _context.Skills.ProjectTo<string>(_mapper.ConfigurationProvider).ToListAsync();
      // return await _context.Skills.ProjectTo<string>(_mapper.ConfigurationProvider).ToListAsync();
      return result;
    }

    public async Task<List<SkillViewModel>> ListAllSkillsAsync()
    {
      return await _context.Skills.ProjectTo<SkillViewModel>(_mapper.ConfigurationProvider).ToListAsync();

    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public async Task UpdateSkillAsync(int id, PostSkillViewModel model)
    {
      var Skill = await _context.Skills.FindAsync(id);

      if (Skill is null) 
      {
        throw new Exception($"Kunde inte hitta skill med id: {id}");
      }
      else{
        Skill.Name = model.Name;
      }

      _context.Skills.Update(Skill);


    }
    }
}