using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoLibrary;

namespace ToDoRestAPI.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class ToDoController : ControllerBase
    {
        private readonly IRepo<ToDoLibrary.Task> _taskRepo;

        public ToDoController(IRepo<ToDoLibrary.Task> taskRepo)
        {
            _taskRepo = taskRepo;
        }

        [ProducesResponseType<List<ToDoLibrary.Task>>(StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_taskRepo.GetAll());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var task = _taskRepo.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [HttpPost]
        public IActionResult AddTask(ToDoLibrary.Task task)
        {
            try
            {
                _taskRepo.Add(task);
                Console.WriteLine(task.ToString());
                return Created();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateTask(int id, ToDoLibrary.Task task)
        {
            var existingTask = _taskRepo.GetById(id);
            if (existingTask == null)
            {
                return NotFound();
            }
            var updatedTask = _taskRepo.Update(id, task);
            return Ok(updatedTask);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        [Route("{id}/complete")]
        public IActionResult CompleteTask(int id)
        {
            var task = _taskRepo.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            task.MarkAsCompleted();
            _taskRepo.Update(id, task);
            return Ok(task);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var deletedTask = _taskRepo.GetById(id);
            if (deletedTask == null)
            {
                return NotFound();
            }
            return Ok(_taskRepo.Remove(id));
        }
    }
}
