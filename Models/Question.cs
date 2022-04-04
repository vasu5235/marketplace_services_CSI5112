using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace marketplace_services_CSI5112.Models
{

    public class Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        [JsonPropertyName("mongoId")] 
        public string? MongoId { get; set; }

        [BsonElement("id")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [BsonElement("title")]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [BsonElement("description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [BsonElement("userName")]
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        public Question(String MongoId, int Id, string Title, string Description, string UserName)
        {
            this.MongoId = MongoId;
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            this.UserName = UserName;
        }
    }
}

