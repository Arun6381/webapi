using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using world.Data;
using world.DTO.Country;
using world.Model;

namespace world.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountryController : ControllerBase
	{
		private readonly ApplicationDbContext _dbContext;

		public CountryController(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status200OK)]

		public ActionResult<IEnumerable<Country>> GetAll()
		{
			var countries = _dbContext.Countries.ToList();
			if (countries==null || countries.Count == 0)
			{
				return new EmptyResult();
			}
			return Ok(countries);
		}

		[HttpGet("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status200OK)]	
		public ActionResult<Country> GetById(int id)
		{
			var country = _dbContext.Countries.Find(id);
			if (country == null)
			{
				return NoContent();
			}
			return Ok(country);
		}


		[HttpPost]
				[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ProducesResponseType(StatusCodes.Status201Created)]	
		public ActionResult<CreateCountryDTO> Create([FromBody] CreateCountryDTO countrydto)
		{
			var result=_dbContext.Countries.AsQueryable()
				.Where(x=>x.Name.ToLower().Trim() == countrydto.Name.ToLower().Trim()).ToList().Any();
			if (result)
			{
				return Conflict("Country Already exits");
			}
			Country country =new Country();
			country.Name = countrydto.Name;
			country.ShortName = countrydto.ShortName;
			country.CountryCode = countrydto.CountryCode;

		    _dbContext.Countries.Add(country);
			_dbContext.SaveChanges();
			return CreatedAtAction("GetById", new { id = country.Id }, countrydto);
		}

		[HttpPut("{id:int}")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<Country> Update(int id,[FromBody] Country country)
		{
			if (country == null||id!=country.Id)
			{
				return BadRequest();
			}
			var countryFromdb = _dbContext.Countries.Find(id);
			if (countryFromdb == null)
			{
				return NotFound();
			}
			countryFromdb.Name = country.Name;
			countryFromdb.ShortName = country.ShortName;
			countryFromdb.CountryCode = country.CountryCode;
			_dbContext.Countries.Update(countryFromdb);
			_dbContext.SaveChanges();
			return NoContent();
		}

		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]

		public ActionResult DeleteById(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var country= _dbContext.Countries.Find( id);
			if (country == null)
			{
				return NotFound();
			}
			_dbContext.Countries.Remove(country);
			_dbContext.SaveChanges();
			return NoContent();
		}
	}
}
