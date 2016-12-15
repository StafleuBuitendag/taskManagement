using CSharp_taskManagement.DataAccess;
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
        private readonly ITaskRepository _taskRepository = new TaskRepository();

        public TasksController()
        {

        }

        public TasksController(ITaskRepository taskRepository)
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
        public HttpResponseMessage Put(int id, Task jsonBody)
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage Post(int id, Task value)
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
