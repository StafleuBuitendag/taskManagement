using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSharp_taskManagement.Controllers
{
    public class Task
    {
        public uint? Id { get; } // populate from db, else null for insert
        public string Name { get; set; }
    }
    public class User
    {
        public uint? Id { get; }
        public string Name { get; set; }
    }

    public interface ITaskRepository
    {

    }

    public class TaskRepository : ITaskRepository
    {

    }

    public class TasksController : ApiController
    {
        private readonly ITaskRepository _tasks = new TaskRepository();

        public TasksController()
        {

        }

        public TasksController(ITaskRepository tasks)
        {
            _tasks = tasks;
        }

        public string[] Get()
        {
            return new string[]
            {
                "asdf",
                "asdsf"
            };
        }

        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {

        }
    }
}
