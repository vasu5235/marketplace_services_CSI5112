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

    //returns all answers in database
    [HttpGet]
    public async Task<ActionResult<List<Answer>>> Get()
    {
        List<Answer> allAnswers = await _answerService.GetAllAnswers();
        if (allAnswers.Count == 0)
            return NotFound("No answers found in database");
        return allAnswers;
    }

    // return all answers for a given questionId.
    [HttpGet("{questionId}")]
    public async Task<ActionResult<List<Answer>>> GetAnswers(int questionId)
    {

        if (questionId == null)
            return BadRequest("questionId cannot be null");

        Console.WriteLine("--- debug ---- question.Id: " + questionId);

        List<Answer> findAllAnswers = await _answerService.GetAnswers(questionId);

        if (findAllAnswers.Count == 0)
            return NotFound("No answers found for this questionId");
        return findAllAnswers;
    }


    //add new answer to database
    [HttpPost]
    public async Task<ActionResult<bool>> AddAnswer(Answer answer)
    {
        //basic null checks
        if (answer.Id == null || answer.Description == null
            || answer.QuestionId == null || answer.UserName == null)
            return BadRequest("One of the body params was found null");

        bool result = await _answerService.AddAnswer(answer);

        if (result == false)
            return BadRequest("Answer with same id exists in db");

        return result;
    }

    //[HttpDelete("{Id}")]
    //public async Task<ActionResult<bool>> DeleteQuestion(int Id)
    //{
    //    bool result = await _questionService.DeleteQuestion(Id);

    //    return result;
    //}
}

