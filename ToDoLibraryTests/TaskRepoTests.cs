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
    public class TaskRepoTests
    {
        private TaskRepo _repo;

        [TestInitialize]
        public void Setup()
        {
            _repo = new TaskRepo();
            _repo.Add(new Task(0, "Initial Task", "This is the initial task.", DateTime.Now.AddDays(2)));
            _repo.Add(new Task(0, "Second Task", "This is the second task.", DateTime.Now.AddDays(3)));
            _repo.Add(new Task(0, "Third Task", "This is the third task.", DateTime.Now.AddDays(4)));
        }

        [TestMethod()]
        public void AddTest()
        {
            _repo.Add(new Task(0, "Test Task", "This is a test task.", DateTime.Now.AddDays(1)));
            Assert.AreEqual(4, _repo.GetAll().Count());
        }

        [TestMethod()]
        public void GetAllTest()
        {
            _repo.GetAll();
            Assert.AreEqual(3, _repo.GetAll().Count());
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            _repo.GetById(2);
            Assert.AreEqual("Second Task", _repo.GetById(2).Title);
        }

        [TestMethod()]
        public void GetByIdTestFail()
        {
            _repo.GetById(99);
            Assert.IsNull(_repo.GetById(99));
        }

        [TestMethod()]
        public void GetByTitleTest()
        {
            _repo.GetByTitle("Third Task");
            Assert.AreEqual(3, _repo.GetByTitle("Third Task").Id);
        }

        [TestMethod()]
        public void RemoveTest()
        {
            Task removedTask = _repo.Remove(1);
            Assert.AreEqual(2, _repo.GetAll().Count());
            Assert.AreEqual("Initial Task", removedTask.Title);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Task taskToUpdate = _repo.GetById(2);
            taskToUpdate.Description = "Updated description.";
            _repo.Update(2, taskToUpdate);
            Assert.AreEqual("Updated description.", _repo.GetById(2).Description);
        }
    }
}