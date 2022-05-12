using MongoDB.Driver;
using System.Text.Json;
using Assignment4.Models;

namespace Assignment4.Services;

public class MongoService {
  
	private readonly MongoClient _client;
	private readonly ILogger<MongoService> _logger;
	private IConfiguration _configuration;
	
	public MongoService(ILogger<MongoService> logger, IConfiguration configuration)
	{
		_configuration = configuration;
		_logger = logger;
		_client = new MongoClient(_configuration.GetValue<string>("ConnectionString"));
		
		_logger.LogInformation("Accessing database");
		
		var db = _client.GetDatabase("hearthstone");
		
		if (_client.GetDatabase("hearthstone").ListCollections().ToList().Count != 0) return;
		
		_logger.LogInformation("Creating collections");
		
		var cardCollection = db.GetCollection<Card>("cards");
		var metaCollection = db.GetCollection<Meta>("meta");

		_logger.LogInformation("Populating collections...");
		_logger.LogInformation("Populating cards");
		
		using var cardFile = new StreamReader("cards.json");

		var cards = JsonSerializer.Deserialize<List<Card>>(cardFile.ReadToEnd(), new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});
		
		if (cards is {Count: > 0})
			_logger.LogInformation("Cards populated with {0} objects", cards.Count);
		else
			_logger.LogInformation("Card population failed - No cards found");

		_logger.LogInformation("Populating metadata");
		
		using var metaFile = new StreamReader("metadata.json");
		
		var metaData = JsonSerializer.Deserialize<Meta>(metaFile.ReadToEnd(), new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});
		
		if (metaData != null)
			_logger.LogInformation("Metadata populated");
		else
			_logger.LogInformation("Metadata population failed - No metadata found");
		
		_logger.LogInformation("Inserting collections");

		cardCollection.InsertMany(cards);
		if (metaData != null) metaCollection.InsertOne(metaData);
		
		_logger.LogInformation("Collections populated - Service is ready");
	}
	public MongoClient Client => _client;
}