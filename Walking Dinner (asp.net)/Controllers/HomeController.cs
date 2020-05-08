using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Walking_Dinner__asp.net_.Models;
using System.Reflection;

namespace Walking_Dinner__asp.net_.Controllers
{

    public class HomeController : Controller
    {

        List<List<string>> persoonlijst;
        List<List<string>> parallelpersoonlijst;
        Random rnd = new Random();
        public ActionResult Pressed()
        {
            return View("addpage");
        }


        [HttpPost]
        public ActionResult Startpage(string agreecheckbox)
        {

            if (agreecheckbox == "check")
            {
                return View("paralleladdpage");
            }
            else
            {
                return View("addpage");
            }

        }
        public ActionResult addpage()
        {

            return View();
        }

        [HttpPost]
        public ActionResult addpage(string addpage)
        {
            if (addpage == "clickedbutton")
            {
            
                return View("startpage");
            }
            else
            {
                return View();
      
            }
        }

        public ActionResult paralleladdpage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult paralleladdpage(string addparallel)
        {

            if (addparallel == "parallelbuttonclicked")
            {

                return View("startpage");
            }
            else
            {
                return View();

            }
        }
    }
}