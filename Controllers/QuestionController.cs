using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace_services_CSI5112.Models;
using marketplace_services_CSI5112.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace marketplace_services_CSI5112.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly QuestionService _questionService;

    public QuestionController(QuestionService qs)
    {
        this._questionService = qs;
    }

    [HttpGet]
    public List<Question> Get()
    {
        return _questionService.GetAllQuestions();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Question>> GetQuestion(int id)
    {
        Console.WriteLine("--- debug ---- question.Id: " + id);

        Question question = await _questionService.GetQuestion(id);
        if (question == null)
            return NotFound();
        return question;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> AddQuestion(Question question)
    {
        bool result = await _questionService.CreateQuestion(question);

        return result;
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult<bool>> DeleteQuestion(int Id)
    {
        bool result = await _questionService.DeleteQuestion(Id);

        return result;
    }
}

