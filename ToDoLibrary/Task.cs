namespace ToDoLibrary
{
    public class Task
    {
        private DateTime _dueDate;
        private string _description;
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }

        public DateTime DueDate {
            get { return _dueDate; }
            set
            {
                if (value < DateTime.Now)
                {
                    throw new ArgumentException("Due date cannot be in the past.");
                }
                _dueDate = value;
            }

        }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string Description {
            get { return _description; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Description cannot be empty.");
                }
                if (value.Length > 255)
                {
                    throw new ArgumentException("Description cannot exceed 255 characters.");
                }
                _description = value;
            }
        }

        public Task() { }

        public Task(int id, string title, string description, DateTime dueDate)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            IsCompleted = false;
        }

        /// <summary>
        /// Marks the task as completed by setting the <see cref="IsCompleted"/> property to <see langword="true"/>.
        /// </summary>
        /// <remarks>This method updates the state of the task to indicate that it has been completed. 
        /// Once marked as completed, the <see cref="IsCompleted"/> property will return <see
        /// langword="true"/>.</remarks>
        public void MarkAsCompleted()
        {
            IsCompleted = true;
        }

        /// <summary>
        /// Updates the due date to a later date if the specified date is later than the current due date.
        /// </summary>
        /// <remarks>If the specified <paramref name="newDueDate"/> is earlier than or equal to the
        /// current due date, the due date remains unchanged.</remarks>
        /// <param name="newDueDate">The new due date to set. Must be later than the current due date.</param>
        public void Postpone(DateTime newDueDate)
        {
            if (newDueDate > DueDate)
            {
                DueDate = newDueDate;
            }
        }

        /// <summary>
        /// Returns a string representation of the task, including its title, due date, and completion status.
        /// </summary>
        /// <returns>A string in the format "Title - Due: [DueDate] - Completed: [IsCompleted]".</returns>
        public override string ToString()
        {
            return $"{Title} - Due: {DueDate.ToShortDateString()} - Completed: {IsCompleted}";
        }
    }
}
