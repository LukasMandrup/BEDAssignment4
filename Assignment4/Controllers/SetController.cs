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

[Route("sets")]
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

	[HttpGet]
	public ActionResult<IEnumerable<Set>> Get()
	{
		return _service.GetSets();
	}
}
