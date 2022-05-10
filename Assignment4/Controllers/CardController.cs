#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment4.Models;
using Assignment4.Services;
using MongoDB.Bson;
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
	public async Task<ActionResult<IEnumerable<Card>>> Get(int? page = 0, int? setid = null, string? artist = null, int? classid = null, int? rarityid = null)
	{
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
	
	
/*
    // GET: api/Card
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Card>>> GetCard()
    {
        return await _context.Card.ToListAsync();
    }

    // GET: api/Card/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Card>> GetCard(int id)
    {
        var card = await _context.Card.FindAsync(id);

        if (card == null)
        {
            return NotFound();
        }

        return card;
    }
    
}
*/
}
