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

        public bool IsEmpty()
        {
            if (Id == null
                && String.IsNullOrEmpty(Name)
                && String.IsNullOrEmpty(Description)
                && String.IsNullOrEmpty(Status)
                && UserId == null) // not worried about dates, because without content, dates are useless.
            {
                return true;
            }
            else
            {
                return false;
            }
        }
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
        bool CreateTask(Task task);
        bool UpdateTask(Task task);
        bool RemoveTask(uint id);
    }

    public class TaskRepository : ITaskRepository
    {
        private string _connectionString = "Data Source=localhost; Port=3306; User ID=root; Password=devbuntu; Database=task_user";
        private MySqlConnection connection;

        public TaskRepository()
            : this("")
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
                            ? (uint?)null
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

        public bool CreateTask(Task task)
        {
            //if (task.Id != null
            //    && !FetchTask((uint)task.Id).IsEmpty())
            //{
            //    UpdateTask(task);
            //}
            //else
            //{
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "Insert into task_user.task (name, description, dueDate) values (@name, @description, @dueDate)";
                command.Parameters.AddWithValue("@name", task.Name);
                command.Parameters.AddWithValue("@description", task.Description);
                command.Parameters.AddWithValue("@dueDate", task.DueDate);

                var rowCount = command.ExecuteNonQuery();

                connection.Close();

                return (rowCount < 1) ? false : true;
            }
            catch (Exception ex)
            {
                if (connection.State.Equals(System.Data.ConnectionState.Open))
                {
                    connection.Close();
                }
                return false;
            }
            //}
        }

        public bool UpdateTask(Task task)
        {
            //if (task.Id == null
            //    && FetchTask((uint)task.Id).IsEmpty())
            //{
            //    CreateTask(task);
            //}
            //else
            //{
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "Update task_user.task set name = @name, description = @description, dueDate = @dueDate, creationDate = @creationDate, status = @status, user_id = @user_id";
                command.Parameters.AddWithValue("@name", task.Name);
                command.Parameters.AddWithValue("@description", task.Description);
                command.Parameters.AddWithValue("@dueDate", task.DueDate);
                command.Parameters.AddWithValue("@creationDate", task.CreationDate);
                command.Parameters.AddWithValue("@status", task.Status);
                command.Parameters.AddWithValue("@user_id", task.UserId);

                var rowCount = command.ExecuteNonQuery();

                connection.Close();

                return (rowCount < 1) ? false : true;
            }
            catch (Exception ex)
            {
                if (connection.State.Equals(System.Data.ConnectionState.Open))
                {
                    connection.Close();
                }
                return false;
            }
            //}
        }

        public bool RemoveTask(uint id)
        {
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "Delete from task_user.task where id = @id";
                command.Parameters.AddWithValue("@id", id);

                var rowCount = command.ExecuteNonQuery();

                connection.Close();

                return (rowCount < 1) ? false : true;
            }
            catch (Exception ex)
            {
                if (connection.State.Equals(System.Data.ConnectionState.Open))
                {
                    connection.Close();
                }
                return false;
            }
        }
    }
}