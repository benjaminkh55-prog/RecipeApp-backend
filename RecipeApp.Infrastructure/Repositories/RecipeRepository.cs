using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.Interfaces;
using RecipeApp.Domain.Entities;
using RecipeApp.Infrastructure.Data;

namespace RecipeApp.Infrastructure.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly RecipeDbContext _context;

    public RecipeRepository(RecipeDbContext context)
    {
        _context = context;
    }

    // GET ALL
    public async Task<List<Recipe>> GetAll()
    {
        return await _context.Recipes
            .Include(r => r.Category)
            .Include(r => r.Ingredients)
            .ToListAsync();
    }

    // GET BY ID
    public async Task<Recipe?> GetById(int id)
    {
        return await _context.Recipes
            .Include(r => r.Category)
            .Include(r => r.Ingredients)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    // CREATE
    public async Task Add(Recipe recipe)
    {
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();
    }
}