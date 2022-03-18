using System;
namespace marketplace_services_CSI5112.Models
{

    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }

        public Answer(int Id, int QuestionId, string Description, string UserName)
        {
            this.Id = Id;
            this.QuestionId = QuestionId;
            this.Description = Description;
            this.UserName = UserName;
        }
    }
}

