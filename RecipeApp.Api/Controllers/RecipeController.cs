using Microsoft.AspNetCore.Mvc;
using RecipeApp.Api.Services;
using RecipeApp.Application.DTOs;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly RecipeService _service;

    public RecipeController(RecipeService service)
    {
        _service = service;
    }

    // =========================
    // GET ALL
    // =========================
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    // =========================
    // GET BY ID
    // =========================
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // =========================
    // POST
    // =========================
    [HttpPost]
    public async Task<IActionResult> Create(CreateRecipeDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    // =========================
    // PUT
    // =========================
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateRecipeDto dto)
    {
        var success = await _service.UpdateAsync(id, dto);

        if (!success)
            return NotFound();

        return NoContent();
    }

    // =========================
    // DELETE
    // =========================
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}
