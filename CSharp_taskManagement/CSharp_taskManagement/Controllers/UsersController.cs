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
    public class UsersController : ApiController
    {
        private readonly IUserTaskRepository _userRepository = new UserRepository();

        public UsersController()
        {

        }

        public UsersController(IUserTaskRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User[] Get()
        {
            var users = _userRepository.FetchUsers();

            return users.ToArray();
        }

        public User Get(uint id)
        {
            var task = _userRepository.FetchUser(id);

            return task;
        }

        [HttpPut]
        public HttpResponseMessage Put(User user)
        {
            var created = _userRepository.CreateUser(user);

            return new HttpResponseMessage((created) ? HttpStatusCode.OK : HttpStatusCode.Forbidden);
        }

        [HttpPost]
        public HttpResponseMessage Post(User user)
        {
            var updated = _userRepository.UpdateUser(user);

            return new HttpResponseMessage((updated) ? HttpStatusCode.OK : HttpStatusCode.Forbidden);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(uint id)
        {
            var removed = _userRepository.RemoveUser(id);

            return new HttpResponseMessage((removed) ? HttpStatusCode.OK : HttpStatusCode.Forbidden);
        }
    }
}
