using Assignment4.Models;
using MongoDB.Driver;

namespace Assignment4.Services;

public class CardService
{
	private readonly IMongoCollection<Card> _collection;
	private readonly ILogger<CardService> _logger;

	public CardService(ILogger<CardService> logger, MongoService service)
	{
		_collection = service.Client.GetDatabase("hearthstone").GetCollection<Card>("cards");
		_logger = logger;
	}
}
