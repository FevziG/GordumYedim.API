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



//Ok(): HTTP 200 - Baþarý, veri döndürebilir.

//Created() / CreatedAtAction(): HTTP 201 - Yeni bir kaynak oluþturuldu.

//BadRequest(): HTTP 400 - Geçersiz istek.

//Unauthorized(): HTTP 401 - Kimlik doðrulama hatasý.

//NotFound(): HTTP 404 - Kaynak bulunamadý.

//Conflict(): HTTP 409 - Çakýþma hatasý.

//NoContent(): HTTP 204 - Baþarý, ancak veri yok.

//StatusCode(int statusCode): Belirli bir HTTP durumu döndürür.

//Redirect() ve RedirectToAction(): Yönlendirme.

//Forbid(): HTTP 403 - Eriþim yasaðý.