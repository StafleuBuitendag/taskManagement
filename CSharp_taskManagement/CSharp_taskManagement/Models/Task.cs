using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSharp_taskManagement.Models
{
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

        public uint? Id { get; set; } // populate from db, else null for insert

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreationDate { get; set; } //populate from db, or default constructor

        public string Status { get; set; }

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
}