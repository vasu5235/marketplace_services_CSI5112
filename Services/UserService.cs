using System;
using marketplace_services_CSI5112.Models;

namespace marketplace_services_CSI5112.Services
{
    public class UserService : Exception
    {

        private readonly List<User> users = new()
        {
            new User(1,"Vasu Mistry","vmist089@uottawa.ca", "123456"),
            new User(2,"Test user","test@uottawa.ca", "123456"),

        };

        public UserService()
        {
        }

        public List<User> GetUsers()
        {
            return this.users;
        }

        public async Task<User> GetUser(string Email)
        {
            return users.Find(x => x.Email.Equals(Email));
        }

        public async Task<Boolean> ValidateUser(string email, string password)
        {
            User user = users.Find(x => x.Email.Equals(email));
            if (user == null)
                return false;

            if (user.Password.Equals(password))
                return true;
            return false;
        }

        // returns false if user exists with newUser.Id.
        public async Task<bool> CreateUser(User newUser)
        {
            User existingUser = users.Find(x => x.Id == newUser.Id);

            if (existingUser == null)
            {
                users.Add(newUser);
                return true;
            }

            return false;

        }
    }
}

