using System.Globalization;
using System.Text.RegularExpressions;
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
    public async Task<ActionResult<List<User>>> Get()
    {
        List<User> allUsers = await _userService.GetUsers();
        if (allUsers.Count == 0)
            return NotFound("No Users in database");

        return allUsers;
    }

    //[HttpPost("{email}/{password}")]
    //public async Task<ActionResult<Boolean>> ValidateUser(String email, String password)
    //{
    //    Console.WriteLine("--- user email and password: " + email + " " + password);

    //    Boolean isValidUser = await _userService.ValidateUser(email, password);
    //    return isValidUser;
    //}

    [HttpPost("{email}/{password}")]
    public async Task<ActionResult<User>> ValidateUser(String email, String password)
    {
        if (email == null || password == null)
            return BadRequest("Either email or password was null");

        if (!IsValidEmail(email))
            return BadRequest("Incorrect email format");

        Console.WriteLine("--- user email and password: " + email + " " + password);

        User user = await _userService.ValidateUser(email, password);
        if (user == null)
            return NotFound("User not found with these credentials");

        return user;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> AddUser(User newUser)
    {
        if (newUser.Email == null || newUser.Name == null
            || newUser.Id == null || newUser.IsMerchant == null
            || newUser.Password == null)
            return NotFound("One of the body fields was null for newUser");

        if (!IsValidEmail(newUser.Email))
            return NotFound("Incorrect email format");

        bool result = await _userService.CreateUser(newUser);

        if (result == false)
            return NotFound("User may already exists with same email or id");
        return result;
    }


    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = System.Text.RegularExpressions.Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                  RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            return false;
        }
        catch (ArgumentException e)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
}