using API;
using Microsoft.EntityFrameworkCore;
using SportStore.Models;

var builder = WebApplication.CreateBuilder();


string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

app.MapGet("/user/login/{login}/{password}", (string login, string password ,ApplicationContext db) => {
    
    ReturnAutor rr = new ReturnAutor();
    User? user = db.User.FirstOrDefault(u => u.Login == login && u.Password == password);
    if (user == null)
    {
        rr.RA = "f";
        return Results.Json(rr);
    }
    else
    {
        rr.RA = "t";
        return Results.Json(rr); 
    }
});

app.Run();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


