using System;
using marketplace_services_CSI5112.Models;

namespace marketplace_services_CSI5112.Services
{
    public class AnswerService
    {
        private List<Answer> answers = new()
        {
            new Answer(1, 1, "Yes I agree, same applies for Winter too.", "Esra Ersan"),
            new Answer(2, 1, "Yes I agree, same applies for Winter too", "Bhavesh Bisth"),
            new Answer(3, 1, "Yes I agree, same applies for Winter too", "Vasu Mistry"),
            new Answer(4, 1, "Yes I agree, same applies for Winter too", "Esra Ersan"),
            new Answer(5, 2, "Yes I agree, same applies for Winter too", "Vraj Baxi"),
            new Answer(6, 2, "Yes I agree, same applies for Winter too", "Vraj Baxi"),
            new Answer(7, 3, "Yes I agree, same applies for Winter too", "Dharmin Sodwadia"),
            new Answer(8, 4, "Yes I agree, same applies for Winter too", "Dharmin Sodwadia"),
            new Answer(9, 5, "Yes I agree, same applies for Winter too", "Rushi Patel"),
            new Answer(10, 6, "Yes I agree, same applies for Winter too", "Rushi Patel"),

        };

        public AnswerService()
        {
        }

        public async Task<List<Answer>> GetAllAnswers()
        {
            return this.answers;
        }
        
        public async Task<List<Answer>> GetAnswers(int QuestionId)
        {
            return answers.FindAll(x => x.QuestionId.Equals(QuestionId));
        }


        // returns false if category exists with newCategory.Id
        public async Task<bool> AddAnswer(Answer newAnswer)
        {
            Answer existingAnswer = answers.Find(x => (x.Id.Equals(newAnswer.Id) && x.QuestionId.Equals(newAnswer.QuestionId)));
            
            if (existingAnswer == null)
            {
                answers.Add(newAnswer);
                return true;
            }

            return false;

        }

        //public async Task<bool> DeleteQuestion(int Id)
        //{
        //    List<Question> q = questions.Where(x => x.Id == Id).ToList();
        //    if (q.Count == 0)
        //        return false;

        //    this.questions = questions.Where(x => ! x.Id.Equals(Id)).ToList();
        //    return true;
        //}
    }
}

