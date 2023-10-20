using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    //BaseApiController luokalla on jo API-Atribuutit, niin niitä ei tarvita enää tähän. Ne siis periytyvät myös.
    public class ActivitiesController : BaseApiController
    {
        private readonly DataContext _context;

        //depedency injection
        public ActivitiesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet] //api/activities
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return await _context.Activities.ToListAsync();
        }

        [HttpGet("{id}")] //api/activities/id
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            return await _context.Activities.FindAsync(id);
        }
    }
}