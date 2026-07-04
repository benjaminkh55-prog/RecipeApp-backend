namespace RecipeApp.Domain.Entities;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    // 1 Category → många Recipes
    public List<Recipe> Recipes { get; set; } = new();
}
