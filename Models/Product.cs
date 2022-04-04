using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace marketplace_services_CSI5112.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        [JsonPropertyName("mongoId")]
        public string? MongoId { get; set; }

        [BsonElement("id")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [BsonElement("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [BsonElement("imageUrl")]
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [BsonElement("description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [BsonElement("category")]
        [JsonPropertyName("category")]
        public string Category { get; set; }

        [BsonElement("price")]
        [JsonPropertyName("price")]
        public double Price { get; set; }

        [BsonElement("quantity")]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        public Product(String MongoId, int Id, string Name, string Description, double Price, string ImageUrl, string Category, int Quantity)
        {
            this.MongoId = MongoId;
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
            this.ImageUrl = ImageUrl;
            this.Category = Category;
            this.Quantity = Quantity;
        }
    }
}

