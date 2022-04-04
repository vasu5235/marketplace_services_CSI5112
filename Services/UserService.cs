using System;
using marketplace_services_CSI5112.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace marketplace_services_CSI5112.Services
{
    public class UserService : Exception
    {

        //private readonly List<User> users = new()
        //{
        //    new User("",1, "Vasu Mistry", "vmist089@uottawa.ca", "123456", false),
        //    new User("",2, "Rushi Patel", "rpate159@uottawa.ca", "654321", false),
        //    new User("",3, "Test user", "test@uottawa.ca", "123456", false),
        //    new User("",4, "Merchant", "merchant@marketplace.ca", "123456", true),

        //};
        private readonly IMongoCollection<User> _users;
        private readonly string CONNECTION_STRING;

        public UserService(IOptions<DatabaseSettings> DatabaseSettings, IConfiguration configuration)
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
            _users = database.GetCollection<User>("User");
        }

        public async Task<List<User>> GetUsers()
        {
            //return this.users;
            System.Console.WriteLine("calling");
            System.Console.WriteLine(this.CONNECTION_STRING);
            System.Diagnostics.Debug.WriteLine("connection string val: " + this.CONNECTION_STRING);
            return await _users.Find(_ => true).ToListAsync();
        }

        public async Task<User> GetUser(string Email)
        {
            //return users.Find(x => x.Email.Equals(Email));
            return await _users.Find(x => x.Email.Equals(Email)).FirstOrDefaultAsync();
        }

        //public async Task<Boolean> ValidateUser(string email, string password)
        //{
        //    User user = users.Find(x => x.Email.Equals(email));
        //    if (user == null)
        //        return false;

        //    if (user.Password.Equals(password))
        //        return true;
        //    return false;
        //}

        public async Task<User> ValidateUser(string email, string password)
        {
            //User user = users.Find(x => x.Email.Equals(email));
            User user = await _users.Find(x => x.Email.Equals(email)).FirstOrDefaultAsync();

            if (user == null)
                return null;

            if (user.Password.Equals(password))
                return user;
            return null;
        }

        // returns false if user exists with newUser.Id.
        public async Task<bool> CreateUser(User newUser)
        {
            //check if user exists
            //User existingUser = users.Find(x => x.Id == newUser.Id || x.Email.Equals(newUser.Email));
            User existingUser = await _users.Find(x => x.Id == newUser.Id || x.Email.Equals(newUser.Email)).FirstOrDefaultAsync();

            if (existingUser == null)
            {
                //users.Add(newUser);
                await _users.InsertOneAsync(newUser);
                return true;
            }

            return false;

        }
    }
}

