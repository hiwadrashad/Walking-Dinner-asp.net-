using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Walking_Dinner__asp.net_.Models
{
    public class adminparallel
    {
        public int id { get; set; }

        public int parallelmaximum { get; set; }

        public string parallelhostpergroep { get; set; }

        public List<string> paralleldeelnemers { get; set; }

        public byte[] parallelpreperatordata { get; set; }

        public byte[] parallelbezoekersdata { get; set; }
    }

    public class AdminparallelDB : DbContext
    {
        public DbSet<adminparallel> data { get; set; }
    }
}