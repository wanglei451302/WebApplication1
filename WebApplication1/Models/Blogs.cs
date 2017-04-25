using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Blogs
    {

        [Key]
        public string id { get; set; }
        public string user_image { get; set; }
        public string name { get; set; }

        [DataType(DataType.MultilineText)]
        public string summary { get; set; }

        [DataType(DataType.MultilineText)]
        public string content { get; set; }

        public string created_at { get; set; }
    }

        public class BlogContext : DbContext
        {
            public DbSet<Blogs> Blogss { get; set; }
        }
    
}