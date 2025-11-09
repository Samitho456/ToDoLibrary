using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoLibrary.Tests
{
    [TestClass()]
    public class TaskTests
    {
        [TestMethod()]
        public void CreateTaskTest()
        {
            Task task = new Task(1, "Test Task", "This is a test task.", DateTime.Now.AddDays(1));

            Assert.AreEqual("Test Task", task.Title);
            Assert.AreEqual("This is a test task.", task.Description);
            Assert.AreEqual(false, task.IsCompleted);
        }

        [TestMethod()]
        public void CompleteTaskTest()
        {
            Task task = new Task(1, "Test Task", "This is a test task.", DateTime.Now.AddDays(1));
            task.MarkAsCompleted();
            Assert.IsTrue(task.IsCompleted);
        }

        [TestMethod()]
        public void PostPoneTest()
        {
            Task task = new Task(1, "Test Task", "This is a test task.", DateTime.Now.AddDays(1));
            DateTime originalDueDate = task.DueDate;
            DateTime newDueDate = originalDueDate.AddDays(2);
            task.Postpone(newDueDate);
            Assert.AreEqual(newDueDate, task.DueDate);
        }
    }
}