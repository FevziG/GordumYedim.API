using Microsoft.EntityFrameworkCore;
using GordumYedim.API.Models;
using static System.Net.WebRequestMethods;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();



//Ok(): HTTP 200 - Ba�ar�, veri d�nd�rebilir.

//Created() / CreatedAtAction(): HTTP 201 - Yeni bir kaynak olu�turuldu.

//BadRequest(): HTTP 400 - Ge�ersiz istek.

//Unauthorized(): HTTP 401 - Kimlik do�rulama hatas�.

//NotFound(): HTTP 404 - Kaynak bulunamad�.

//Conflict(): HTTP 409 - �ak��ma hatas�.

//NoContent(): HTTP 204 - Ba�ar�, ancak veri yok.

//StatusCode(int statusCode): Belirli bir HTTP durumu d�nd�r�r.

//Redirect() ve RedirectToAction(): Y�nlendirme.

//Forbid(): HTTP 403 - Eri�im yasa��.