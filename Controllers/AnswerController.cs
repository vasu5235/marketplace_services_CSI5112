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
public class AnswerController : ControllerBase
{
    private readonly AnswerService _answerService;

    public AnswerController(AnswerService _as)
    {
        this._answerService = _as;
    }

    [HttpGet]
    public async Task<ActionResult<List<Answer>>> Get()
    {
        return await _answerService.GetAllAnswers();
    }

    [HttpGet("{questionId}")]
    public async Task<ActionResult<List<Answer>>> GetAnswers(int questionId)
    {
        Console.WriteLine("--- debug ---- question.Id: " + questionId);

        List<Answer> findAllAnswers = await _answerService.GetAnswers(questionId);
        if (findAllAnswers == null)
            return NotFound();
        return findAllAnswers;
    }

    [HttpPost]
    public async Task<ActionResult<bool>> AddAnswer(Answer answer)
    {
        bool result = await _answerService.AddAnswer(answer);

        return result;
    }

    //[HttpDelete("{Id}")]
    //public async Task<ActionResult<bool>> DeleteQuestion(int Id)
    //{
    //    bool result = await _questionService.DeleteQuestion(Id);

    //    return result;
    //}
}

