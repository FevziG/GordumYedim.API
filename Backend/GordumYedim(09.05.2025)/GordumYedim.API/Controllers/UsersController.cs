using GordumYedim.API.Models;
using GordumYedim.API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GordumYedim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase 
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context) 
        {
            _context = context;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Users.ToListAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
        {
            var data = await _context.Users.FindAsync(id);
            if (data == null) { return NotFound(); }
            return Ok(data);   
        }

        //----------------------------------------------------------------

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto model)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == model.Username || u.Email == model.Email);
            if (existingUser == null)
            {
                var newUser = new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    UserCity = model.UserCity,
                    Email = model.Email
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();
                return Ok(new {success= true});
            }
            else 
            { 
                return Conflict(new { message= "Bu kullanıcı adı zaten alınmış",success= false});
            }
        }

        //----------------------------------------------------------------

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model) 
        {
            var user =_context.Users.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            if (user == null) 
            { 
                return NotFound(new {message="Giriş bilgileri hatalı", success=false}); 
            };
            return Ok(new { success = true, userId = user.UserId, 
            message = "Giriş başarılı."});
        }


        [HttpPut("editProfile")]
        public async Task<IActionResult> editProfile([FromBody]RegisterDto model) 
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == model.UserId);
            user.Username = model.Username;
            user.Password = model.Password;
            user.UserCity = model.UserCity;
            user.Email = model.Email;

            _context.SaveChanges();
            return Ok(new { success = true });


        }
    }
}
