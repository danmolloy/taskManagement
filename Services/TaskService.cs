using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Services
{
  public class TaskService
  {
    private readonly TaskDbContext _context;
    private readonly UserManager<IdentityUser>_userManager;

    public TaskService(TaskDbContext context, UserManager<IdentityUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }

    public async Task<List<TaskItem>> GetUserTasksAsync(IdentityUser user)
    {
      return await _context.Tasks
      .Where(task => task.UserId == user.Id)
      .ToListAsync();
    }

    public async Task UpdateTaskCompleted(int taskID)
    {
      Console.WriteLine("Hello, world");
      var task = await _context.Tasks.FindAsync(taskID);
      task!.IsCompleted = !task.IsCompleted;
      await _context.SaveChangesAsync();
    }


    public async Task AddTaskAsync(TaskItem task, IdentityUser user)
    {
      task.UserId = user.Id;
      _context.Tasks.Add(task);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(TaskItem task)
    {
      _context.Tasks.Remove(task);
      await _context.SaveChangesAsync();
    }
  }
}