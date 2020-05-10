using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Walking_Dinner__asp.net_.Models
{
    public class admin
    {
        public int id { get; set; }

        public int maximum { get; set; }

        public string hostpergroep { get; set; }

        public List<string> deelnemers { get; set; }

        public byte[] preperatordata { get; set; }

        public byte[] bezoekersdata { get; set; }


    }

    public class AdminDB : DbContext
    {
        public DbSet<admin> data { get; set; }
    }
}