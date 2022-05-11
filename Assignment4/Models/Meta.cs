using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Assignment4.Models;

public class Meta
{
	[BsonId]
	public ObjectId Id { get; set; }
	public List<Set> Sets { get; set; }
	public List<Rarity> Rarities { get; set; }
	public List<Class> Classes { get; set; }
	[JsonPropertyName("Types")]
	public List<CardType> CardType { get; set; }
}

[BsonNoId]
public class Set
{
	[JsonPropertyName("Id")]
	public int Id { get; set; }
	public string Name { get; set; }
	public string Type { get; set; }
	[JsonPropertyName("collectibleCount")]
	public int CardCount { get; set; }
}

public class CardType
{
	public int Id { get; set; }
	public string Name { get; set; }
}

public class Class
{
	public int Id { get; set; }
	public string Name { get; set; }
}

public class Rarity
{
	public int Id { get; set; }
	public string Name { get; set; }
}