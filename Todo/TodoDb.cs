namespace Todo;

using Microsoft.EntityFrameworkCore;
using Todo;


public class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options) : base(options) { }

    public DbSet<TodoItem> TodoItems { get; set; }
}