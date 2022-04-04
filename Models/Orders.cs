using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace marketplace_services_CSI5112.Models
{
    public class Orders
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        [JsonPropertyName("mongoId")]
        public string? MongoId { get; set; }

        [BsonElement("id")]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [BsonElement("products")]
        [JsonPropertyName("products")]
        public List<Product> ProductList;

        public Orders(String MongoId, string Id,  List<Product> ProductList)
        {
            this.MongoId = MongoId;
            this.Id = Id;
            this.ProductList = ProductList;
        }
    }
}

