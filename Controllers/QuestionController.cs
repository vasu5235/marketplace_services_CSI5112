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

    //returns all questions
    [HttpGet]
    public async Task<ActionResult<List<Question>>> Get()
    {
        List<Question> allQuestions = await _questionService.GetAllQuestions();

        if (allQuestions.Count == 0)
            return NotFound("No Questions exists currently in database");
        return allQuestions;
    }

    //returns question for a given questionId
    [HttpGet("{id}")]
    public async Task<ActionResult<Question>> GetQuestion(int id)
    {
        if (id == null)
            return NotFound("questionId cannot be null");

        Console.WriteLine("--- debug ---- question.Id: " + id);

        Question question = await _questionService.GetQuestion(id);
        if (question == null)
            return NotFound("No Question found for this id");

        return question;
    }

    //adds new question
    [HttpPost]
    public async Task<ActionResult<bool>> AddQuestion(Question question)
    {
        if (question.Id == null || question.Title == null || question.Description == null || question.UserName == null)
            return NotFound("Error: One of the body parameters in null");
        bool result = await _questionService.CreateQuestion(question);

        if (result == false)
            return BadRequest("Question with id already exists");
        return result;
    }

    //delete question with given questionId
    [HttpDelete("{Id}")]
    public async Task<ActionResult<bool>> DeleteQuestion(int Id)
    {
        if (Id == null)
            return NotFound("Error: One of the body parameters in null");

        bool result = await _questionService.DeleteQuestion(Id);

        if (result == false)
            return BadRequest("Question with id does not exists");

        return result;
    }
}

