using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Assignment4.Models;

public class Card
{
	[BsonId]
	public int Id { get; set; }
	public string Name { get; set; }
	public int ClassId { get; set; }

	[JsonPropertyName("cardTypeId")]
	public int TypeId { get; set; }
	
	[JsonPropertyName("cardSetId")]
	public int SetId { get; set; }
	
	public int? SpellSchoolId { get; set; }
	public int RarityId { get; set; }
	public int? Health { get; set; }
	public int? Attack { get; set; }
	public int ManaCost { get; set; }
	
	[JsonPropertyName("artistName")]
	public string Artist { get; set; }
	public string Text { get; set; }
	public string FlavorText { get; set; }
}

public class CardDTO
{
	[BsonId]
	public int Id { get; set; }
	public string Name { get; set; }
	public string Class { get; set; }

	public string Type { get; set; }
	
	public string Set { get; set; }
	
	public int? SpellSchoolId { get; set; }
	public string Rarity { get; set; }
	public int? Health { get; set; }
	public int? Attack { get; set; }
	public int ManaCost { get; set; }
	
	[JsonPropertyName("artistName")]
	public string Artist { get; set; }
	public string Text { get; set; }
	public string FlavorText { get; set; }
}