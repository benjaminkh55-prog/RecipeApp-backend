using RecipeApp.Domain.Entities;

namespace RecipeApp.Application.Interfaces;

public interface IRecipeRepository
{
    Task<List<Recipe>> GetAll();
    Task<Recipe?> GetById(int id);
    Task Add(Recipe recipe);
}
