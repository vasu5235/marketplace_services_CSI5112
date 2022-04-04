using System;
using marketplace_services_CSI5112.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace marketplace_services_CSI5112.Services
{
    public class QuestionService
    {
        //private List<Question> questions = new()
        //{
        //    new Question("",1, "First Question", "A suit of armor provides excellent sun protection on hot days.", "Esra Ersan"),
        //    new Question("",2, "Second Question", "A suit of armor provides excellent sun protection on hot days.", "Bhavesh Bisth"),
        //    new Question("", 3, "Third Question", "A suit of armor provides excellent sun protection on hot days.", "Vasu Mistry"),
        //    new Question("", 4, "Fourth Question", "A suit of armor provides excellent sun protection on hot days.", "Rushi Patel"),
        //    new Question("", 5, "Fifth Question", "A suit of armor provides excellent sun protection on hot days.", "Dharmin Sodwadia"),
        //    new Question("", 6, "Sixth Question", "A suit of armor provides excellent sun protection on hot days.", "Vraj Baxi")

        //};
        private readonly IMongoCollection<Question> _questions;
        private readonly string CONNECTION_STRING;

        public QuestionService(IOptions<DatabaseSettings> DatabaseSettings, IConfiguration configuration)
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
            _questions = database.GetCollection<Question>("Question");
        }

        public async Task<List<Question>> GetAllQuestions()
        {
            //System.Diagnostics.Debug.WriteLine("=======MONGODB question" + _questions.Find(_ => true).ToListAsync().ToString());
            //return this.questions;
            return await _questions.Find(_ => true).ToListAsync();
        }
        
        public async Task<Question> GetQuestion(int Id)
        {
            //return questions.Find(x => x.Id == Id);
            return await _questions.Find(x => x.Id == Id).FirstOrDefaultAsync();
        }


        // returns false if category exists with newQuestion.Id
        public async Task<bool> CreateQuestion(Question newQuestion)
        {
            //check if question exists
            //Question existingQuestion = questions.Find(x => x.Id == newQuestion.Id);
            Question existingQuestion = await _questions.Find(x => x.Id == newQuestion.Id).FirstOrDefaultAsync();

            if (existingQuestion == null)
            {
                //questions.Add(newQuestion);
                await _questions.InsertOneAsync(newQuestion);

                //test code to check update functionality in mongodb, can be deleted later.
                //Question existingQuestion2 = await _questions.Find(x => x.Id == 1).FirstOrDefaultAsync();
                //newQuestion.MongoId = existingQuestion2.MongoId;

                //ReplaceOneResult r = await _questions.ReplaceOneAsync(x => x.Id == 1, newQuestion);
                //if (r.IsModifiedCountAvailable)
                //    System.Diagnostics.Debug.WriteLine("=======MONGODB update question" + r.ModifiedCount);
                //else
                //    System.Diagnostics.Debug.WriteLine("=======MONGODB update FAILED");

                
                
                return true;
            }

            return false;

        }

        public async Task<bool> DeleteQuestion(int Id)
        {
            //check if question exists
            //List<Question> q = questions.Where(x => x.Id == Id).ToList();
            Question q =  await _questions.Find(x => x.Id == Id).FirstOrDefaultAsync();
            if (q == null)
                return false;

            //this.questions = questions.Where(x => !(x.Id == Id)).ToList();
            DeleteResult result = await _questions.DeleteOneAsync(x => x.Id == Id); 
            return result.DeletedCount == 1;
        }
    }
}

