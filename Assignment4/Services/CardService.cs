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
	}

	public async Task<List<CardDTO>> Get(FilterDefinition<Card> filter)
	{
		var cards = await _cardCollection.Find(filter).ToListAsync();
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

		return translatedCards;
	}
}
