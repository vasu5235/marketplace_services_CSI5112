using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace marketplace_services_CSI5112.Models
{
	public class User
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

		[BsonElement("email")]
		[JsonPropertyName("email")]
		public string Email { get; set; }

		[BsonElement("password")]
		[JsonPropertyName("password")]
		public string Password { get; set; }

		[BsonElement("isMerchant")]
		[JsonPropertyName("isMerchant")]
		public Boolean IsMerchant { get; set; }

		public User(String MongoId, int Id, string Name, string Email, string Password, Boolean IsMerchant) 
		{
			this.MongoId = MongoId;
			this.Id = Id;
			this.Name = Name;
			this.Email = Email;
			this.Password = Password;
			this.IsMerchant = IsMerchant;
		}
	}
}

