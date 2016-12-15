using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CSharp_taskManagement.DataAccess
{
    //[DataContract]
    public class Task
    {
        public Task()
        {

        }
        public Task(uint Id, DateTime CreationDate)
        {
            this.Id = Id;
            this.CreationDate = CreationDate;
        }
        //[DataMember]
        public uint? Id { get; set; } // populate from db, else null for insert
        //[DataMember]
        public string Name { get; set; }
        //[DataMember]
        public string Description { get; set; }
        //[DataMember]
        public DateTime DueDate { get; set; }
        //[DataMember]
        public DateTime CreationDate { get; set; } //populate from db, or default constructor
        //[DataMember]
        public string Status { get; set; }
        //[DataMember]
        public uint? UserId { get; set; }
    }
    //[DataContract]
    public class User
    {
        public User()
        {

        }
        public User(uint Id, DateTime DateLastLogin)
        {

        }

        //[DataMember]
        public uint? Id { get; }
        //[DataMember]
        public string Name { get; set; }
        //[DataMember]
        public string Email { get; set; }
        //[DataMember]
        public string Password { get; set; }
        //[DataMember]
        public DateTime DateLastLogin { get; }
    }

    public interface ITaskRepository
    {
        List<Task> FetchTasks();
        Task FetchTask(uint id);
    }

    public class TaskRepository : ITaskRepository
    {
        private string _connectionString = "Data Source=localhost; Port=3306; User ID=root; Password=devbuntu; Database=task_user";
        private MySqlConnection connection;

        public TaskRepository()
            :this("")
        {

        }

        public TaskRepository(string connectionString)
        {
            if (!String.IsNullOrEmpty(connectionString))
            {
                _connectionString = connectionString;
            }
            connection = new MySqlConnection(_connectionString);
        }
        
        public List<Task> FetchTasks()
        {
            try
            {
                connection.Open();

                var tasks = new List<Task>();

                var command = connection.CreateCommand();
                command.CommandText = "Select * from task_user.task";

                using (var reader = command.ExecuteReader())
                {
                    int namePosition = reader.GetOrdinal("name");
                    int descriptionPosition = reader.GetOrdinal("description");
                    int dueDatePosition = reader.GetOrdinal("dueDate");
                    int statusPosition = reader.GetOrdinal("status");
                    int userIdPosition = reader.GetOrdinal("user_id");
                    while (reader.Read())
                    {
                        var task = new Task(reader.GetUInt32("id"), reader.GetDateTime("creationDate"));
                        task.Name = reader.IsDBNull(namePosition) 
                            ? string.Empty 
                            : reader.GetString("name");
                        task.Description = reader.IsDBNull(descriptionPosition)
                            ? string.Empty 
                            : reader.GetString("description");
                        task.DueDate = reader.IsDBNull(dueDatePosition)
                            ? new DateTime()
                            : reader.GetDateTime("dueDate");
                        task.Status = reader.IsDBNull(statusPosition)
                            ? string.Empty
                            : reader.GetString("status");
                        task.UserId = reader.IsDBNull(userIdPosition)
                            ? (uint?) null
                            : reader.GetUInt32("user_id");
                        tasks.Add(task);
                    }
                }

                connection.Close();

                return tasks;
            }
            catch (Exception ex)
            {
                if (connection.State.Equals(System.Data.ConnectionState.Open))
                {
                    connection.Close();
                }
            }
            return new List<Task>();
        }

        public Task FetchTask(uint id)
        {
            try
            {
                connection.Open();

                var task = new Task();

                var command = connection.CreateCommand();
                command.CommandText = "Select * from task_user.task where id = @id";
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    int namePosition = reader.GetOrdinal("name");
                    int descriptionPosition = reader.GetOrdinal("description");
                    int dueDatePosition = reader.GetOrdinal("dueDate");
                    int statusPosition = reader.GetOrdinal("status");
                    int userIdPosition = reader.GetOrdinal("user_id");

                    reader.Read();

                    task = new Task(reader.GetUInt32("id"), reader.GetDateTime("creationDate"));
                    task.Name = reader.IsDBNull(namePosition)
                        ? string.Empty
                        : reader.GetString("name");
                    task.Description = reader.IsDBNull(descriptionPosition)
                        ? string.Empty
                        : reader.GetString("description");
                    task.DueDate = reader.IsDBNull(dueDatePosition)
                        ? new DateTime()
                        : reader.GetDateTime("dueDate");
                    task.Status = reader.IsDBNull(statusPosition)
                        ? string.Empty
                        : reader.GetString("status");
                    task.UserId = reader.IsDBNull(userIdPosition)
                        ? (uint?)null
                        : reader.GetUInt32("user_id");
                }

                connection.Close();

                return task;
            }
            catch (Exception ex)
            {
                if (connection.State.Equals(System.Data.ConnectionState.Open))
                {
                    connection.Close();
                }
            }
            return new Task();
        }
    }
}