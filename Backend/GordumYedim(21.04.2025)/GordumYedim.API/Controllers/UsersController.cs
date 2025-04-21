using GordumYedim.API.Models;
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
        public async Task<IActionResult> Register(string username, string password, string usercity, string email)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == username || u.Email == email);
            if (existingUser == null)
            {
                var newUser = new User
                {
                    Username = username,
                    Password = password,
                    UserCity = usercity,
                    Email = email
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();
                return Ok(newUser);
            }
            else 
            { 
                return Conflict(new { message= "Bu kullanıcı adı zaten alınmış",success= false});
            }
        }

        //----------------------------------------------------------------

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password) 
        {
            var user =_context.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
            if (user == null) { return NotFound(new {message="Giriş bilgileri hatalı", success=false}); };
            return Ok(new { success = true, 
            message = "Giriş başarılı."});
        }

        [HttpPut("editProfile")]
        public async Task<IActionResult> editProfile(int ıd,string username, string password,
            string usercity, string email) 
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == ıd);
            user.Username = username;
            user.Password = password;
            user.UserCity = usercity;
            user.Email = email;

            _context.SaveChanges();
            return Ok(new { success = true });


        }
    }
}
