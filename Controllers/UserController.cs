using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static List<User> users = new();
    private static int nextId = 1;

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            return NotFound(new { message = "User not found." });
        return Ok(user);
    }

    [HttpPost]
    public IActionResult Create([FromBody] User user)
    {
        if (string.IsNullOrWhiteSpace(user.FullName))
            return BadRequest(new { message = "FullName is required." });

        if (string.IsNullOrWhiteSpace(user.Email) || !new EmailAddressAttribute().IsValid(user.Email))
            return BadRequest(new { message = "A valid Email is required." });

        user.Id = nextId++;
        users.Add(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }
}