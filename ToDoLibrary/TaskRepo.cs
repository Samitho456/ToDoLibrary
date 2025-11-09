using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoLibrary
{
    public class TaskRepo : IRepo<Task>
    {
        private int _nextId = 1;
        private List<Task> tasks = new List<Task>();
        public TaskRepo() { }

        /// <summary>
        /// Adds a new task to the repository and assigns it a unique ID.
        /// </summary>
        /// <param name="task">The new Task object to be added</param>
        public void Add(Task task)
        {
            task.Id = _nextId++;
            tasks.Add(task);
        }

        /// <summary>
        /// Retrieves all tasks in the collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Task"/> objects representing the tasks in the collection.</returns>
        public IEnumerable<Task> GetAll()
        {
            return tasks;
        }

        /// <summary>
        /// Retrieves all tasks sorted by the specified property in either ascending or descending order.
        /// </summary>
        /// <remarks>The sorting is case-insensitive for the <paramref name="sortBy"/> parameter. If the
        /// specified property name is not recognized, the tasks are sorted by their ID in the specified
        /// order.</remarks>
        /// <param name="sortBy">The name of the property to sort by. Valid values are "title", "created", "duedate", "iscompleted", or any
        /// other property name. Sorting defaults to the task ID if an invalid or unsupported property name is provided.</param>
        /// <param name="decending">A boolean value indicating whether the sorting should be in descending order. <see langword="true"/> for
        /// descending order; otherwise, <see langword="false"/> for ascending order. The default is <see
        /// langword="false"/>.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of tasks sorted by the specified property and order.</returns>
        public IEnumerable<Task> GetAllSorted(string sortBy, bool decending = false)
        {
            var query = tasks.AsQueryable();

            return sortBy.ToLower() switch
            {
                "title" => decending ? query.OrderByDescending(t => t.Title) : query.OrderBy(t => t.Title),
                "created" => decending ? query.OrderByDescending(t => t.CreatedAt) : query.OrderBy(t => t.CreatedAt),
                "duedate" => decending ? query.OrderByDescending(t => t.DueDate) : query.OrderBy(t => t.DueDate),
                "iscompleted" => decending ? query.OrderByDescending(t => t.IsCompleted) : query.OrderBy(t => t.IsCompleted),
                _ => decending ? query.OrderByDescending(t => t.Id) : query.OrderBy(t => t.Id),
            };
        }

        /// <summary>
        /// Retrieves a task with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to retrieve.</param>
        /// <returns>The task with the specified identifier, or <see langword="null"/> if no matching task is found.</returns>
        public Task GetById(int id) 
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Retrieves the first task that matches the specified title.
        /// </summary>
        /// <param name="name">The title of the task to search for. This parameter cannot be <see langword="null"/> or empty.</param>
        /// <returns>The first task with a title that matches the specified name, or <see langword="null"/> if no such task is
        /// found.</returns>
        public Task GetByTitle(string name) 
        { 
            return tasks.FirstOrDefault(t => t.Title == name);
        }

        /// <summary>
        /// Removes the task with the specified identifier from the collection.
        /// </summary>
        /// <param name="id">The unique identifier of the task to remove.</param>
        /// <returns>The task that was removed, or <see langword="null"/> if no task with the specified identifier exists.</returns>
        public Task Remove(int id)
        {
            var task = GetById(id);
            if (task != null)
            {
                tasks.Remove(task);
            }
            return task;
        }

        /// <summary>
        /// Updates the details of an existing task with the specified identifier.
        /// </summary>
        /// <remarks>This method modifies the properties of the existing task to match those of the
        /// <paramref name="updatedTask"/> object. If no task with the specified identifier is found, no changes are
        /// made, and the method returns <see langword="null"/>.</remarks>
        /// <param name="id">The unique identifier of the task to update.</param>
        /// <param name="updatedTask">An object containing the updated task details. The properties of this object will replace the corresponding
        /// properties of the existing task.</param>
        /// <returns>The updated task if the task with the specified identifier exists; otherwise, <see langword="null"/>.</returns>
        public Task Update(int id, Task updatedTask)
        {
            var existingTask = GetById(id);
            if (existingTask != null)
            {
                existingTask.Title = updatedTask.Title;
                existingTask.Description = updatedTask.Description;
                existingTask.DueDate = updatedTask.DueDate;
                existingTask.IsCompleted = updatedTask.IsCompleted;
            }
            return existingTask;
        }
    }
}
