using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace marketplace_services_CSI5112.Models
{

    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        [JsonPropertyName("mongoId")]
        public string? MongoId { get; set; }

        [BsonElement("id")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [BsonElement("imageURL")]
        [JsonPropertyName("imageURL")]
        public string ImageURL { get; set; }

        [BsonElement("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        public Category(String MongoId, int Id, string Name, string ImageURL)
        {
            this.MongoId = MongoId;
            this.Id = Id;
            this.ImageURL = ImageURL;
            this.Name = Name;
        }
    }
}

