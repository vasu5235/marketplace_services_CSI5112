using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace marketplace_services_CSI5112.Models
{

    public class Answer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        [JsonPropertyName("mongoId")]
        public string? MongoId { get; set; }

        [BsonElement("id")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [BsonElement("questionId")]
        [JsonPropertyName("questionId")]
        public int QuestionId { get; set; }

        [BsonElement("description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [BsonElement("userName")]
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        public Answer(String MongoId, int Id, int QuestionId, string Description, string UserName)
        {
            this.MongoId = MongoId;
            this.Id = Id;
            this.QuestionId = QuestionId;
            this.Description = Description;
            this.UserName = UserName;
        }
    }
}

