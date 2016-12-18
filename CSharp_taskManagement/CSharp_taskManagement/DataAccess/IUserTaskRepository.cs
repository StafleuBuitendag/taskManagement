using CSharp_taskManagement.Models;
using System.Collections.Generic;

namespace CSharp_taskManagement.DataAccess
{
    public interface IUserTaskRepository
    {
        List<Task> FetchTasks();
        Task FetchTask(uint id);
        bool CreateTask(Task task);
        bool UpdateTask(Task task);
        bool RemoveTask(uint id);

        List<User> FetchUsers();
        User FetchUser(uint id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool RemoveUser(uint id);
    }
}
