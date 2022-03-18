using System;
using marketplace_services_CSI5112.Models;

namespace marketplace_services_CSI5112.Services
{
    public class QuestionService
    {
        private List<Question> questions = new()
        {
            new Question(1, "First Question", "A suit of armor provides excellent sun protection on hot days.", "Esra Ersan"),
            new Question(2, "Second Question", "A suit of armor provides excellent sun protection on hot days.", "Bhavesh Bisth"),
            new Question(3, "Third Question", "A suit of armor provides excellent sun protection on hot days.", "Vasu Mistry"),
            new Question(4, "Fourth Question", "A suit of armor provides excellent sun protection on hot days.", "Rushi Patel"),
            new Question(5, "Fifth Question", "A suit of armor provides excellent sun protection on hot days.", "Dharmin Sodwadia"),
            new Question(6, "Sixth Question", "A suit of armor provides excellent sun protection on hot days.", "Vraj Baxi")

        };

        public QuestionService()
        {
        }

        public List<Question> GetAllQuestions()
        {
            return this.questions;
        }
        
        public async Task<Question> GetQuestion(int Id)
        {
            return questions.Find(x => x.Id.Equals(Id));
        }


        // returns false if category exists with newCategory.Id
        public async Task<bool> CreateQuestion(Question newQuestion)
        {
            Question existingQuestion = questions.Find(x => x.Id.Equals(newQuestion.Id));
            
            if (existingQuestion == null)
            {
                questions.Add(newQuestion);
                return true;
            }

            return false;

        }

        public async Task<bool> DeleteQuestion(int Id)
        {
            List<Question> q = questions.Where(x => x.Id == Id).ToList();
            if (q.Count == 0)
                return false;

            this.questions = questions.Where(x => ! x.Id.Equals(Id)).ToList();
            return true;
        }
    }
}

