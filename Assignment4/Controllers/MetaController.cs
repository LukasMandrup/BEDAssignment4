#nullable disable
using Microsoft.AspNetCore.Mvc;
using Assignment4.Models;
using Assignment4.Services;

namespace Assignment4.Controllers;

[ApiController]
public class SetController : ControllerBase
{
	private readonly MetaService _service;
	private readonly ILogger<SetController> _logger;

	public SetController(ILogger<SetController> logger, MetaService service)
	{
		_service = service;
		_logger = logger;
	}

	[HttpGet("sets")]
	public ActionResult<IEnumerable<Set>> GetSets()
	{
		return _service.GetSets();
	}
	
	[HttpGet("rarities")]
	public ActionResult<IEnumerable<Rarity>> GetRarities()
	{
		return _service.GetRarities();
	}
	
	[HttpGet("classes")]
	public ActionResult<IEnumerable<Class>> GetClasses()
	{
		return _service.GetClasses();
	}
	
	[HttpGet("types")]
	public ActionResult<IEnumerable<CardType>> GetTypes()
	{
		return _service.GetTypes();
	}
}
