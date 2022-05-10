using Assignment4.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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
    }

    public List<Set> GetSets()
    {
        return _collection.Find(new BsonDocument()).FirstOrDefault().Sets;
    }
    
    public List<Rarity> GetRarities()
    {
        return _collection.Find(new BsonDocument()).FirstOrDefault().Rarities;
    }
    
    public List<Class> GetClasses()
    {
        return _collection.Find(new BsonDocument()).FirstOrDefault().Classes;
    }
    
    public List<CardType> GetTypes()
    {
        return _collection.Find(new BsonDocument()).FirstOrDefault().CardType;
    }
}