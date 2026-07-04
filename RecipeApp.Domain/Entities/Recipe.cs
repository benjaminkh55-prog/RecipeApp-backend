using RecipeApp.Domain.Entities;

namespace RecipeApp.Domain.Entities;

public class Recipe
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    // Foreign Key
    public int CategoryId { get; set; }

    // Navigation Property (valfri)
    public Category? Category { get; set; }

    // Navigation Property
    public List<Ingredient> Ingredients { get; set; } = new();
}