using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CSharp_taskManagement.Models;

namespace CSharp_taskManagement.DataAccess
{
    public class UserRepository : IUserTaskRepository
    {
        private string _connectionString = "Data Source=localhost; Port=3306; User ID=root; Password=devbuntu; Database=task_user";
        private MySqlConnection connection;

        public UserRepository()
            : this("")
        {

        }

        public UserRepository(string connectionString)
        {
            if (!String.IsNullOrEmpty(connectionString))
            {
                _connectionString = connectionString;
            }
            connection = new MySqlConnection(_connectionString);
        }
        
        public List<User> FetchUsers()
        {
            try
            {
                connection.Open();

                var users = new List<User>();

                var command = connection.CreateCommand();
                command.CommandText = "Select * from task_user.task_user";

                using (var reader = command.ExecuteReader())
                {
                    int namePosition = reader.GetOrdinal("name");
                    int surnamePosition = reader.GetOrdinal("surname");
                    int statusPosition = reader.GetOrdinal("status");
                    int emailPosition = reader.GetOrdinal("email");
                    int creationDatePosition = reader.GetOrdinal("creationDate");

                    while (reader.Read())
                    {
                        var user = new User(reader.GetUInt32("id"), reader.GetDateTime("dateLastLogin"));
                        user.Name = reader.IsDBNull(namePosition)
                            ? string.Empty
                            : reader.GetString("name");
                        user.Surname = reader.IsDBNull(surnamePosition)
                            ? string.Empty
                            : reader.GetString("surname");
                        user.Status = reader.IsDBNull(statusPosition)
                            ? string.Empty
                            : reader.GetString("status");
                        user.Email = reader.IsDBNull(emailPosition)
                            ? string.Empty
                            : reader.GetString("email");
                        user.CreationDate = reader.IsDBNull(creationDatePosition)
                            ? new DateTime()
                            : reader.GetDateTime("creationDate");
                        users.Add(user);
                    }
                }

                connection.Close();

                return users;
            }
            catch (Exception ex)
            {
                if (connection.State.Equals(System.Data.ConnectionState.Open))
                {
                    connection.Close();
                }
            }
            return new List<User>();
        }

        public User FetchUser(uint id)
        {
            try
            {
                connection.Open();

                var user = new User();

                var command = connection.CreateCommand();
                command.CommandText = "Select * from task_user.task_user where id = @id";
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    int namePosition = reader.GetOrdinal("name");
                    int surnamePosition = reader.GetOrdinal("surname");
                    int statusPosition = reader.GetOrdinal("status");
                    int emailPosition = reader.GetOrdinal("email");
                    int creationDatePosition = reader.GetOrdinal("creationDate");

                    reader.Read();

                    user = new User(reader.GetUInt32("id"), reader.GetDateTime("dateLastLogin"));
                    user.Name = reader.IsDBNull(namePosition)
                        ? string.Empty
                        : reader.GetString("name");
                    user.Surname = reader.IsDBNull(surnamePosition)
                        ? string.Empty
                        : reader.GetString("surname");
                    user.Status = reader.IsDBNull(statusPosition)
                        ? string.Empty
                        : reader.GetString("status");
                    user.Email = reader.IsDBNull(emailPosition)
                        ? string.Empty
                        : reader.GetString("email");
                    user.CreationDate = reader.IsDBNull(creationDatePosition)
                        ? new DateTime()
                        : reader.GetDateTime("creationDate");
                }

                connection.Close();

                return user;
            }
            catch (Exception ex)
            {
                if (connection.State.Equals(System.Data.ConnectionState.Open))
                {
                    connection.Close();
                }
            }
            return new User();
        }

        public bool CreateUser(User user)
        {
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "Insert into task_user.task_user (name, surname, status, email) values (@name, @surname, @status, @email)";
                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@surname", user.Surname);
                command.Parameters.AddWithValue("@status", user.Status);
                command.Parameters.AddWithValue("@email", user.Email);

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

        public bool UpdateUser(User user)
        {
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "Update task_user.task_user set name = @name, surname = @surname, status = @status, password = @password, email = @email, dateLastLogin = @dateLastLogin";
                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@surname", user.Surname);
                command.Parameters.AddWithValue("@status", user.Status);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@dateLastLogin", user.DateLastLogin);

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

        public bool RemoveUser(uint id)
        {
            try
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "Delete from task_user.user where id = @id";
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


        public List<Task> FetchTasks()
        {
            throw new NotImplementedException();
        }

        public Task FetchTask(uint id)
        {
            throw new NotImplementedException();
        }

        public bool CreateTask(Task task)
        {
            throw new NotImplementedException();
        }

        public bool UpdateTask(Task task)
        {
            throw new NotImplementedException();
        }

        public bool RemoveTask(uint id)
        {
            throw new NotImplementedException();
        }
    }
}