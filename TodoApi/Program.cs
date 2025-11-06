// using Microsoft.EntityFrameworkCore;
// using TodoApi;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container
// builder.Services.AddDbContext<ToDoDbContext>(options =>
//     options.UseMySql(builder.Configuration.GetConnectionString("ToDoDB"),
//     ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ToDoDB"))));

// // Add CORS
// builder.Services.AddCors();

// var app = builder.Build();

// // Configure CORS
// app.UseCors(builder => builder
//     .AllowAnyOrigin()
//     .AllowAnyMethod()
//     .AllowAnyHeader());

// // דף הבית
// app.MapGet("/", () => "ברוכים הבאים ל-Todo API! השתמשו ב-/api/todos כדי לגשת למשימות");

// // קבלת כל המשימות
// app.MapGet("/api/todos", async (ToDoDbContext db) =>
//     await db.Items.ToListAsync());

// // קבלת משימה לפי מזהה
// app.MapGet("/api/todos/{id}", async (int id, ToDoDbContext db) =>
//     await db.Items.FindAsync(id)
//         is Item item
//             ? Results.Ok(item)
//             : Results.NotFound());

// // הוספת משימה חדשה
// app.MapPost("/api/todos", async (Item item, ToDoDbContext db) =>
// {
//     db.Items.Add(item);
//     await db.SaveChangesAsync();
//     return Results.Created($"/api/todos/{item.Id}", item);
// });

// // עדכון משימה קיימת
// app.MapPut("/api/todos/{id}", async (int id, Item inputItem, ToDoDbContext db) =>
// {
//     var item = await db.Items.FindAsync(id);
//     if (item is null) return Results.NotFound();

//     item.Title = inputItem.Title;
//     item.Description = inputItem.Description;
//     item.IsCompleted = inputItem.IsCompleted;

//     await db.SaveChangesAsync();
//     return Results.NoContent();
// });

// // מחיקת משימה
// app.MapDelete("/api/todos/{id}", async (int id, ToDoDbContext db) =>
// {
//     var item = await db.Items.FindAsync(id);
//     if (item is null) return Results.NotFound();

//     db.Items.Remove(item);
//     await db.SaveChangesAsync();
//     return Results.Ok(item);
// });

// app.Run();
using Microsoft.EntityFrameworkCore;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ToDoDB"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ToDoDB"))));

// Add CORS
builder.Services.AddCors();

// הוספת Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// הפעלת Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Configure CORS
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// דף הבית
app.MapGet("/", () => "ברוכים הבאים ל-Todo API! השתמשו ב-/api/todos כדי לגשת למשימות");

// קבלת כל המשימות
app.MapGet("/api/todos", async (ToDoDbContext db) =>
    await db.Items.ToListAsync());

// קבלת משימה לפי מזהה
app.MapGet("/api/todos/{id}", async (int id, ToDoDbContext db) =>
    await db.Items.FindAsync(id)
        is Item item
            ? Results.Ok(item)
            : Results.NotFound());

// הוספת משימה חדשה
app.MapPost("/api/todos", async (Item item, ToDoDbContext db) =>
{
    db.Items.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/api/todos/{item.Id}", item);
});

// עדכון משימה קיימת
app.MapPut("/api/todos/{id}", async (int id, Item inputItem, ToDoDbContext db) =>
{
    var item = await db.Items.FindAsync(id);
    if (item is null) return Results.NotFound();

    item.Title = inputItem.Title;
    item.Description = inputItem.Description;
    item.IsCompleted = inputItem.IsCompleted;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

// מחיקת משימה
app.MapDelete("/api/todos/{id}", async (int id, ToDoDbContext db) =>
{
    var item = await db.Items.FindAsync(id);
    if (item is null) return Results.NotFound();

    db.Items.Remove(item);
    await db.SaveChangesAsync();
    return Results.Ok(item);
});

app.Run();
