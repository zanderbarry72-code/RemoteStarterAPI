using Microsoft.EntityFrameworkCore;
using RemoteStarterAPI.Data;
using RemoteStarterAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// HTTP only (no HTTPS issues)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
});

// Services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// GET all users
app.MapGet("/users", async (AppDbContext db) =>
    await db.Users.ToListAsync());

// GET user by ID
app.MapGet("/users/{id}", async (int id, AppDbContext db) =>
{
    var user = await db.Users.FindAsync(id);
    return user != null ? Results.Ok(user) : Results.NotFound();
});

// POST a new user
app.MapPost("/users", async (AppDbContext db, User user) =>
{
    db.Users.Add(user);
    await db.SaveChangesAsync();
    return Results.Created($"/users/{user.Id}", user);
});

// PUT (update) user
app.MapPut("/users/{id}", async (int id, AppDbContext db, User updatedUser) =>
{
    var user = await db.Users.FindAsync(id);
    if (user == null) return Results.NotFound();

    user.Name = updatedUser.Name;
    user.Email = updatedUser.Email;

    await db.SaveChangesAsync();
    return Results.Ok(user);
});

// DELETE user
app.MapDelete("/users/{id}", async (int id, AppDbContext db) =>
{
    var user = await db.Users.FindAsync(id);
    if (user == null) return Results.NotFound();

    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();