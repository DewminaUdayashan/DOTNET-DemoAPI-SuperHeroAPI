using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        [HttpGet]
        public async Task<ActionResult<List<SuperHero> >> Get()
        {
            var hero = await _dataContext.SuperHeros.ToListAsync();
            return  Ok(hero);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _dataContext.SuperHeros.FindAsync(id);
            if (hero == null) return BadRequest(NotFound());
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _dataContext.SuperHeros.Add(hero);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeros.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = await _dataContext.SuperHeros.FindAsync(request.Id);
            if (hero == null) return BadRequest(NotFound());

            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName=request.LastName;
           await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeros.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            var hero = await _dataContext.SuperHeros.FindAsync(id);
            if (hero == null) return BadRequest(NotFound());
            _dataContext.SuperHeros.Remove(hero);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeros.ToListAsync());
        }


    }
}
