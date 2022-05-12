using System.Diagnostics;
using Assignment4.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Assignment4.Services;

public class CardService
{
	private readonly IMongoCollection<Card> _cardCollection;
	private readonly IMongoCollection<Meta> _metaCollection;
	
	private readonly ILogger<CardService> _logger;

	public CardService(ILogger<CardService> logger, MongoService service)
	{
		_cardCollection = service.Client.GetDatabase("hearthstone").GetCollection<Card>("cards");
		_metaCollection = service.Client.GetDatabase("hearthstone").GetCollection<Meta>("meta");
		
		_logger = logger;
		
		_logger.LogInformation("CardService created");
	}

	public async Task<List<CardDTO>> Get(FilterDefinition<Card> filter, int? page, int pageSize)
	{
		List<Card> cards;
		
		// If page isn't .1, return page, else return all cards
		if (page != -1)
			cards = await _cardCollection.Find(filter).Skip(pageSize * page).Limit(pageSize).ToListAsync();
		else
			cards = await _cardCollection.Find(filter).ToListAsync();
		
		var translatedCards = new List<CardDTO>();

		foreach (var card in cards)
		{
			var typeName = _metaCollection
				               .Find(new BsonDocument())
				               ?.FirstOrDefault().CardType
				               .Find(type => type.Id == card?.TypeId)
				               ?.Name;
			
			var rarityName = _metaCollection
				.Find(new BsonDocument())
				?.FirstOrDefault().Rarities
				.Find(r => r.Id == card?.RarityId)
				?.Name;
			
			
			var setName = _metaCollection
				.Find(new BsonDocument())
				?.FirstOrDefault().Sets
				.Find(s => s.Id == card?.SetId)
				?.Name;
			
			var className = _metaCollection
				.Find(new BsonDocument())
				?.FirstOrDefault().Classes
				.Find(c => c.Id == card?.ClassId)
				?.Name;

			translatedCards.Add(new CardDTO
			{
				Id = card.Id,
				Name = card.Name,
				Class = className ?? "",
				Type = typeName ?? "",
				Set = setName ?? "",
				SpellSchoolId = card.SpellSchoolId,
				Rarity = rarityName ?? "",
				Health = card.Health,
				Attack = card.Attack,
				ManaCost = card.ManaCost,
				Artist = card.Artist,
				Text = card.Text,
				FlavorText = card.FlavorText
			});
		}
		
		_logger.LogInformation("{count} cards found", translatedCards.Count);

		return translatedCards;
	}
}
