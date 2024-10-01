using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TaskManagement.Models;

namespace TaskManagement.Data 
{
public class TaskDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<TaskItem> Tasks { get; set; }

    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<TaskItem>()
        .HasOne(task => task.User)
        .WithMany()
        .HasForeignKey(task => task.UserId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}

}