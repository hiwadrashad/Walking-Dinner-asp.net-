using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Data.Entity;

namespace Walking_Dinner__asp.net_.Models
{
    public class persoondataparallel
    {
        public int id { get; set; }

        public string parallelvolledigenaam { get; set; }
        public string parallelstraatnaam { get; set; }

        public string parallelhuisnummer { get; set; }

        public string parallelpostcode { get; set; }

        public string parallelplaats { get; set; }

        public string paralleltelefoonnummer { get; set; }

        public string parallelemailadress { get; set; }

        public string paralleldieetwensen { get; set; }

        public string parallelvolledigenaam2 { get; set; }
        public string parallelstraatnaam2 { get; set; }

        public string parallelhuisnummer2 { get; set; }

        public string parallelpostcode2 { get; set; }

        public string parallelplaats2 { get; set; }

        public string paralleltelefoonnummer2 { get; set; }

        public string parallelemailadress2 { get; set; }

        public string paralleldieetwensen2 { get; set; }

        public string parallelgroep { get; set; }

        public bool parallelisgroepvol { get; set; }

        public bool parallelgastheergweeest { get; set; }
    }

    public class persoonparallelDB : DbContext
    {
        public DbSet<persoondataparallel> dataparallel { get; set; }
    }
}