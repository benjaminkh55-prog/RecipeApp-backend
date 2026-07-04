namespace RecipeApp.Application.DTOs;

public class RecipeDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Category { get; set; } = "";
    public List<string> Ingredients { get; set; } = new();
}
