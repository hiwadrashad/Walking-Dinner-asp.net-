using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace Walking_Dinner__asp.net_.Models
{
    public class persoondata
    {
        public int id { get; set; }

        public string volledigenaam { get; set; }
        public string straatnaam { get; set; }

        public string huisnummer { get; set; }

        public string postcode { get; set; }

        public string plaats { get; set; }

        public string telefoonnummer { get; set; }

        public string emailadress { get; set; }

        public string dieetwensen { get; set; }

        public string volledigenaam2 { get; set; }
        public string straatnaam2 { get; set; }

        public string huisnummer2 { get; set; }

        public string postcode2 { get; set; }

        public string plaats2 { get; set; }

        public string telefoonnummer2 { get; set; }

        public string emailadress2 { get; set; }

        public string dieetwensen2 { get; set; }

        public string groep { get; set; }

        public bool isgroepvol { get; set; }

        public bool gastheergeweest { get; set; }
    }

    public class persoonDB : DbContext
    {
        public DbSet<persoondata> data { get; set; }
    }
}