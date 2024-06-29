// DEPENDENCY INJECTION
using Microsoft.EntityFrameworkCore;
using Todo;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));

// RESTFUL DEFINE
var app = builder.Build();

app.MapGet("/todos", (TodoDb db) => db.TodoItems.ToList());

app.MapGet("/todos/{id}", (int id, TodoDb db) => db.TodoItems.Find(id));

app.MapPost("/todos", (TodoItem item, TodoDb db) => {
    db.TodoItems.Add(item);
    db.SaveChanges();
    return Results.Created($"/todosasd/{item.Id}", item);
});

app.MapPatch("/todos/{id}", (int id, TodoItem item, TodoDb db) => {
    TodoItem dbItem = db.TodoItems.Find(id);
    if (dbItem == null) return Results.NotFound();
    dbItem.Name = item.Name;
    dbItem.IsCompleted = item.IsCompleted;
    db.SaveChanges();
    return Results.NoContent();
});

app.MapDelete("/todos/{id}", (int id, TodoDb db) => {
    TodoItem dbItem = db.TodoItems.Find(id);
    if (dbItem == null) return Results.NotFound();
    db.Remove(dbItem);
    db.SaveChanges();
    return Results.NoContent();
});

app.Run();
