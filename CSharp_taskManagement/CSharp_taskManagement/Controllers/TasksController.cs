using CSharp_taskManagement.DataAccess;
using CSharp_taskManagement.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Http;

namespace CSharp_taskManagement.Controllers
{
    public class TasksController : ApiController
    {
        private readonly IUserTaskRepository _taskRepository = new TaskRepository();

        public TasksController()
        {

        }

        public TasksController(IUserTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task[] Get()
        {
            var tasks = _taskRepository.FetchTasks();

            return tasks.ToArray();
        }

        public Task Get(uint id)
        {
            var task = _taskRepository.FetchTask(id);

            return task;
        }

        [HttpPut]
        public HttpResponseMessage Put(Task task)
        {
            var created = _taskRepository.CreateTask(task);

            return new HttpResponseMessage((created) ? HttpStatusCode.OK : HttpStatusCode.Forbidden);
        }

        [HttpPost]
        public HttpResponseMessage Post(Task task)
        {
            var updated = _taskRepository.UpdateTask(task);

            return new HttpResponseMessage((updated) ? HttpStatusCode.OK : HttpStatusCode.Forbidden);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(uint id)
        {
            var removed = _taskRepository.RemoveTask(id);

            return new HttpResponseMessage((removed) ? HttpStatusCode.OK : HttpStatusCode.Forbidden);
        }
    }
}
