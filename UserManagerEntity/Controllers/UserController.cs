using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using UserManagerEntity.Contract.Requests;
using UserManagerEntity.Models;
using UserManagerEntity.Models.Entities;
using UserManagerEntity.Services;
namespace UserManagerEntity.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly DataContext _context;
    private readonly RoleStore<Role> _roleStore;
    private readonly UserManager<User> _userManager;
    private readonly IUserServices _userService;

    public UserController(UserManager<User> userManager, DataContext context, IUserServices userServices)
    {
        _userManager = userManager;
        _context = context;
        _roleStore = new RoleStore<Role>(_context);
        _userService = userServices;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound($"User {id} is not found.");
        }
        return Ok(user);
    }
}
