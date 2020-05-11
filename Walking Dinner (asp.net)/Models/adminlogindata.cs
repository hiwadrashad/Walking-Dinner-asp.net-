using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Antlr.Runtime.Tree;

namespace Walking_Dinner__asp.net_.Models
{
    public class adminlogindata
    {
        public int id { get; set; }

        public string loginnaam { get; set; }

        public string wachtwoord { get; set; }

    }
    public class AdminloginDB : DbContext
    {
        public DbSet<adminlogindata> data { get; set; }
    }
}