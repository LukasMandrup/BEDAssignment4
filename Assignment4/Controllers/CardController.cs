#nullable disable
using Microsoft.AspNetCore.Mvc;
using Assignment4.Models;
using Assignment4.Services;
using MongoDB.Driver;

namespace Assignment4.Controllers;

[Route("cards")]
[ApiController]
public class CardController : ControllerBase
{
	private readonly CardService _service;
	private readonly ILogger<CardController> _logger;

	public CardController(ILogger<CardController> logger, CardService service)
	{
		_service = service;
		_logger = logger;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<CardDTO>>> Get(int? page = 0, int? setid = null, string? artist = null, int? classid = null, int? rarityid = null)
	{
		_logger.LogInformation("Getting cards - page: {page}, setid: {setid}, artist: {artist}, classid: {classid}, rarityid: {rarityid}", page, setid, artist, classid, rarityid);
		
		var filter = Builders<Card>.Filter.Empty;

		if (setid != null)
			filter &= Builders<Card>.Filter.Eq(c => c.SetId, setid);
		
		if (artist != null)
			filter &= Builders<Card>.Filter.Eq(c => c.Artist, artist);
		
		if (classid != null)
			filter &= Builders<Card>.Filter.Eq(c => c.ClassId, classid);
		
		if (rarityid != null)
			filter &= Builders<Card>.Filter.Eq(c => c.RarityId, rarityid);

		return await _service.Get(filter);
	}
}
