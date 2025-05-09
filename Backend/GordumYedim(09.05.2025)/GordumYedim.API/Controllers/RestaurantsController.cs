using Microsoft.AspNetCore.Mvc;
using GordumYedim.API.Models;
using Microsoft.EntityFrameworkCore;
namespace GordumYedim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RestaurantsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Restaurants.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) return NotFound();
            return Ok(restaurant);
        }



        [HttpPost]
        public async Task<IActionResult> PostRes([FromBody] Restaurant newRes)
        {
            newRes.CreatedTime = DateTime.Now;
            _context.Restaurants.Add(newRes);
            await _context.SaveChangesAsync();
            return Ok(new { success = true });
        }


        //[HttpPost]
        //public async Task<IActionResult> PostRes(string resName, string resAddress,
        //    decimal latitude, decimal longitude, string placeId) 
        //{
        //    var newRes = new Restaurant 
        //    {
        //        ResName = resName,
        //        ResAddress = resAddress,
        //        Latitude = latitude,
        //        Longitude = longitude,  
        //        PlaceId = placeId,
        //        CreatedTime = DateTime.Now
        //    };
        //    _context.Restaurants.Add(newRes);
        //    _context.SaveChanges();
        //    return Ok(new {success =true});
        //}
    }
}