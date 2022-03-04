using marketplace_services_CSI5112.Models;
using marketplace_services_CSI5112.Services;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_services_CSI5112.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService us)
    {
        this._userService = us;
    }

    [HttpGet]
    public List<User> Get()
    {
        return _userService.GetUsers();
    }

    [HttpGet("{email}/{password}")]
    public async Task<ActionResult<Boolean>> ValidateUser(String email, String password)
    {
        //Console.WriteLine("--- user email and password: " + email + " " + password);

        Boolean isValidUser = await _userService.ValidateUser(email, password);
        return isValidUser;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> AddUser(User newUser)
    {
        bool result = await _userService.CreateUser(newUser);

        return result;
    }
}