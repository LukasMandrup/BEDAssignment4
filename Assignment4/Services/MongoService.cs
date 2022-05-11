using MongoDB.Driver;
using System.Text.Json;
using Assignment4.Models;

namespace Assignment4.Services;

public class MongoService {
  
	private readonly MongoClient _client;
	
	public MongoService() 
	{
		_client = new MongoClient("mongodb://localhost:27017/");
		var db = _client.GetDatabase("hearthstone");

		if (_client.GetDatabase("hearthstone").ListCollections().ToList().Count != 0) return;
		
		var cardCollection = db.GetCollection<Card>("cards");
		var metaCollection = db.GetCollection<Meta>("meta");

		using var cardFile = new StreamReader("cards.json");

		var cards = JsonSerializer.Deserialize<List<Card>>(cardFile.ReadToEnd(), new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});
		
		using var metaFile = new StreamReader("metadata.json");
		
		var metaData = JsonSerializer.Deserialize<Meta>(metaFile.ReadToEnd(), new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});
		
		cardCollection.InsertMany(cards);
		if (metaData != null) metaCollection.InsertOne(metaData);
	}
	public MongoClient Client => _client;
}