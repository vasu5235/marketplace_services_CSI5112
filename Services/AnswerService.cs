using System;
using marketplace_services_CSI5112.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace marketplace_services_CSI5112.Services
{
    public class AnswerService
    {
        //private List<Answer> answers = new()
        //{
        //    new Answer("",1, 1, "Yes I agree, same applies for Winter too.", "Esra Ersan"),
        //    new Answer("", 2, 1, "Yes I agree, same applies for Winter too", "Bhavesh Bisth"),
        //    new Answer("", 3, 1, "Yes I agree, same applies for Winter too", "Vasu Mistry"),
        //    new Answer("", 4, 1, "Yes I agree, same applies for Winter too", "Esra Ersan"),
        //    new Answer("", 5, 2, "Yes I agree, same applies for Winter too", "Vraj Baxi"),
        //    new Answer("", 6, 2, "Yes I agree, same applies for Winter too", "Vraj Baxi"),
        //    new Answer("", 7, 3, "Yes I agree, same applies for Winter too", "Dharmin Sodwadia"),
        //    new Answer("", 8, 4, "Yes I agree, same applies for Winter too", "Dharmin Sodwadia"),
        //    new Answer("", 9, 5, "Yes I agree, same applies for Winter too", "Rushi Patel"),
        //    new Answer("", 10, 6, "Yes I agree, same applies for Winter too", "Rushi Patel"),

        //};
        private readonly IMongoCollection<Answer> _answers;
        private readonly string CONNECTION_STRING;

        public AnswerService(IOptions<DatabaseSettings> DatabaseSettings, IConfiguration configuration)
        {
            string CONNECTION_STRING = configuration.GetValue<string>("CONNECTION_STRING");

            if (string.IsNullOrEmpty(CONNECTION_STRING))
            {
                // default - should not be used
                System.Console.WriteLine("*********WARNING: USING DEFAULT CONNECTION STRING!!!!!*********");
                System.Diagnostics.Debug.WriteLine("*********WARNING: USING DEFAULT CONNECTION STRING!!!!!*********");
                CONNECTION_STRING = "mongodb+srv://admin_user:QKVmntvNB1QgVBe3@cluster0.pd3bf.mongodb.net/Marketplace?retryWrites=true&w=majority";
            }
            else
            {
                System.Console.WriteLine("*********SUCCESS: USING DB STRING FROM ENV!!!!!*********");
                System.Diagnostics.Debug.WriteLine("*********SUCCESS: USING DB STRING FROM ENV!!!!!*********");
            }
            this.CONNECTION_STRING = CONNECTION_STRING;

            var settings = MongoClientSettings.FromConnectionString(CONNECTION_STRING);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("Marketplace");
            _answers = database.GetCollection<Answer>("Answer");
        }

        public async Task<List<Answer>> GetAllAnswers()
        {
            //return this.answers;
            return await _answers.Find(_ => true).ToListAsync();
        }
        
        public async Task<List<Answer>> GetAnswers(int QuestionId)
        {
            //return answers.FindAll(x => x.QuestionId.Equals(QuestionId));
            return await _answers.Find(x => x.QuestionId == QuestionId).ToListAsync();
        }


        // returns false if category exists with newCategory.Id
        public async Task<bool> AddAnswer(Answer newAnswer)
        {
            //check if answer exists
            //Answer existingAnswer = answers.Find(x => (x.Id == newAnswer.Id));
            Answer existingAnswer = await _answers.Find(x => x.Id == newAnswer.Id).FirstOrDefaultAsync();
            
            if (existingAnswer == null)
            {
                //answers.Add(newAnswer);
                await _answers.InsertOneAsync(newAnswer);
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

