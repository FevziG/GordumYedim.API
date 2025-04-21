using GordumYedim.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GordumYedim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase 
    {
        private readonly AppDbContext _context;
        public CommentsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var data = await _context.Comments.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>Get(int id)
        {
            var data = await _context.Comments.FindAsync(id);
            if (data == null) { return NotFound(); }
            return Ok(data);
        }

        //-----------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> SendComment(int userId,int resId,
            int rating,string comment,IFormFile photo) 
        {
            var com = new Comment
            {
                CommentUserId= userId,
                CommentResId = resId,
                Rating =rating,
                Comment1 = comment,
                CommentTime = DateTime.Now
                
            };
            using (var ms = new MemoryStream()) 
            {
                photo.CopyTo(ms);
                com.Image = ms.ToArray();
            }
            _context.Comments.Add(com);
            _context.SaveChanges();
            return Ok(com);
        }

        [HttpPut]
        public async Task<IActionResult> EditComment(int id, int rating, string comment, IFormFile photo)
        {
            var com = await _context.Comments.FindAsync(id);
            if(com == null) { return NotFound(); }
            com.Rating = rating;
            com.Comment1 = comment;
            com.CommentTime = DateTime.Now;
            using var ms = new MemoryStream();
            {   photo.CopyTo(ms);
                com.Image = ms.ToArray();
            }
            _context.SaveChanges();
            return Ok();
        }

    }

}
