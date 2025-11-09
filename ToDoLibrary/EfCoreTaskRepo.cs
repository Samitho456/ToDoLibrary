using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoLibrary
{
    public class EfCoreTaskRepo : IRepo<Task>
    {
        private readonly ToDoContext _context;

        public EfCoreTaskRepo(ToDoContext context)
        {
            _context = context;
        }

        public void Add(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public IEnumerable<Task> GetAll()
        {
            return _context.Tasks.ToList();
        }

        public Task GetById(int id)
        {
            return _context.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public Task GetByTitle(string title)
        {
            return _context.Tasks.FirstOrDefault(t => t.Title == title);
        }

        public Task Remove(int id)
        {
            var taskToRemove = GetById(id);
            if (taskToRemove != null)
            {
                _context.Tasks.Remove(taskToRemove);
                _context.SaveChanges();
            }
            return taskToRemove;
        }

        public Task Update(int id, Task updatedTask)
        {
            var existingTask = GetById(id);
            if (existingTask != null)
            {
                existingTask.Title = updatedTask.Title;
                existingTask.Description = updatedTask.Description;
                existingTask.DueDate = updatedTask.DueDate;
                existingTask.IsCompleted = updatedTask.IsCompleted;
                _context.SaveChanges();
            }
            return existingTask;
        }
    }
}
