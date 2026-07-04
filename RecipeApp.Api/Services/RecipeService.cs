using Microsoft.EntityFrameworkCore;
using RecipeApp.Application.DTOs;
using RecipeApp.Infrastructure.Data;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Api.Services;

public class RecipeService
{
    private readonly RecipeDbContext _context;

    public RecipeService(RecipeDbContext context)
    {
        _context = context;
    }

    // =========================
    // GET ALL
    // =========================
    public async Task<List<RecipeDto>> GetAllAsync()
    {
        return await _context.Recipes
            .Include(r => r.Category)
            .Include(r => r.Ingredients)
            .Select(r => new RecipeDto
            {
                Id = r.Id,
                Title = r.Title,
                Description = r.Description,
                Category = r.Category.Name,
                Ingredients = r.Ingredients.Select(i => i.Name).ToList()
            })
            .ToListAsync();
    }

    // =========================
    // GET BY ID
    // =========================
    public async Task<RecipeDto?> GetByIdAsync(int id)
    {
        var recipe = await _context.Recipes
            .Include(r => r.Category)
            .Include(r => r.Ingredients)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (recipe == null) return null;

        return new RecipeDto
        {
            Id = recipe.Id,
            Title = recipe.Title,
            Description = recipe.Description,
            Category = recipe.Category.Name,
            Ingredients = recipe.Ingredients.Select(i => i.Name).ToList()
        };
    }

    // =========================
    // CREATE (POST)
    // =========================
    public async Task<Recipe> CreateAsync(CreateRecipeDto dto)
    {
        var recipe = new Recipe
        {
            Title = dto.Title,
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            Ingredients = dto.Ingredients.Select(i => new Ingredient
            {
                Name = i
            }).ToList()
        };

        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();

        return recipe;
    }

    // =========================
    // UPDATE (PUT)
    // =========================
    public async Task<bool> UpdateAsync(int id, CreateRecipeDto dto)
    {
        var recipe = await _context.Recipes
            .Include(r => r.Ingredients)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (recipe == null) return false;

        recipe.Title = dto.Title;
        recipe.Description = dto.Description;
        recipe.CategoryId = dto.CategoryId;

        recipe.Ingredients.Clear();

        recipe.Ingredients = dto.Ingredients.Select(i => new Ingredient
        {
            Name = i
        }).ToList();

        await _context.SaveChangesAsync();
        return true;
    }

    // =========================
    // DELETE
    // =========================
    public async Task<bool> DeleteAsync(int id)
    {
        var recipe = await _context.Recipes.FindAsync(id);

        if (recipe == null) return false;

        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();

        return true;
    }
}