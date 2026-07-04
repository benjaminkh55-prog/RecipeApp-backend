
namespace RecipeApp.Domain.Entities;

public class Ingredient
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Amount { get; set; } = string.Empty;

    // FK → Recipe
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}
