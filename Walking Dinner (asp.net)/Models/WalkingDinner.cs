using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Walking_Dinner__asp.net_.Models
{
    public class WalkingDinner
    {
        List<Groep> groepen { get; set; }
    }


    public class Groep
    {
        List<Persoon> Persoon { get; set; }
    }

    public class Persoon
    {
        bool HasHosted { get; set; } = false;
    }
}