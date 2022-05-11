using Assignment4.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Assignment4.Services;

public class MetaService
{
    private readonly IMongoCollection<Meta> _collection;
    private readonly ILogger<CardService> _logger;

    public MetaService(ILogger<CardService> logger, MongoService service)
    {
        _collection = service.Client.GetDatabase("hearthstone").GetCollection<Meta>("meta");
        _logger = logger;
        
        _logger.LogInformation("MetaService created");
    }

    public List<Set> GetSets()
    {
	    var sets = _collection.Find(new BsonDocument()).FirstOrDefault().Sets;

	    _logger.LogInformation("Getting {count} rarities", sets.Count);

	    return sets;
    }
    
    public List<Rarity> GetRarities()
    {
	    var rarities = _collection.Find(new BsonDocument()).FirstOrDefault().Rarities;

	    _logger.LogInformation("Getting {count} rarities", rarities.Count);
	    
	    return rarities;
    }
    
    public List<Class> GetClasses()
    {
	    var classes = _collection.Find(new BsonDocument()).FirstOrDefault().Classes;

	    _logger.LogInformation("Getting {count} classes", classes.Count);
	    
	    return classes;
    }
    
    public List<CardType> GetTypes()
    {
	    var cardTypes = _collection.Find(new BsonDocument()).FirstOrDefault().CardType;
	    
	    _logger.LogInformation("Getting {count} card types", cardTypes.Count);
	    
	    return cardTypes;
    }
}