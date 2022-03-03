using Microsoft.AspNetCore.Mvc;

namespace marketplace_services_CSI5112.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloWorldController : ControllerBase
{

    [HttpGet]
    public String Get()
    { 
        return new string("Hello World");
    } 
}