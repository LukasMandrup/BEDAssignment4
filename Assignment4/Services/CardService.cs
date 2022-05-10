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

	public async Task<List<Card>> Get(FilterDefinition<Card> filter)
	{
		var cards = await _collection.Find(filter).ToListAsync();
		var translatedCards = new List<CardDTO>();

		foreach (var card in cards)
		{
			translatedCards.Add(new CardDTO
			{
				Id = card.Id,
				Name = card.Name
			};
		}

		return cards;
	}
}
