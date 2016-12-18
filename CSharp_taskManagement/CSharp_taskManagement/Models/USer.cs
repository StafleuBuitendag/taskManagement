using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSharp_taskManagement.Models
{
    public class User
    {
        public User()
        {

        }
        public User(uint Id, DateTime DateLastLogin)
        {
            this.Id = Id;
            this.DateLastLogin = DateLastLogin;
        }

        public uint? Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime DateLastLogin { get; set; }

        public string Status { get; set; }

        public DateTime CreationDate { get; internal set; }
    }
}