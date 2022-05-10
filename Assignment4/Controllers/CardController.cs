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

[Route("api/[controller]")]
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
/*
    // GET: api/Card
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Card>>> GetCard()
    {
        return await _context.Card.ToListAsync();
    }

    // GET: api/Card/5
    [HttpGet("{setId},{artist}")]
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
