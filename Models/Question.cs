using System;
namespace marketplace_services_CSI5112.Models
{

    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }

        public Question(int Id, string Title, string Description, string UserName)
        {
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            this.UserName = UserName;
        }
    }
}

