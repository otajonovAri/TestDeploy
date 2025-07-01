using DeployTest.Data;
using DeployTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeployTest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUser()
    {
        var users = await context.Users.ToListAsync();
        return Ok(users);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
            return NotFound($"User with ID {id} not found.");
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostUser([FromBody] User user)
    {
        if (user is null)
            return BadRequest("User object is null.");

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, [FromBody] User user)
    {
        if (user is null)
            return BadRequest("User object is null.");

        var existingUser = await context.Users.FindAsync(id);
        if (existingUser == null)
            return NotFound($"User with ID {id} not found.");

        context.Entry(existingUser).CurrentValues.SetValues(user);
        await context.SaveChangesAsync();
        return Ok(existingUser);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
            return NotFound($"User with ID {id} not found.");

        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return NoContent(); 
    }
}
