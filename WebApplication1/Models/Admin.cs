using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Admin
    {
        [Key]
        [DataType(DataType.Password)]
        public string name { get; set; }
        [DataType(DataType.Password)]
        public string passwd { get; set; }
        public DateTime created_at { get; set; }
        public string salt { get; set; }
    }

    public class AdminContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
    }
}