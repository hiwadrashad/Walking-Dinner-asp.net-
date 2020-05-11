using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Walking_Dinner__asp.net_.Models;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System.Net.Mail;
using System.Web.UI.WebControls.WebParts;
using System.Security.Principal;
using System.Web.ModelBinding;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Ajax.Utilities;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Walking_Dinner__asp.net_.Documents;
using Walking_Dinner__asp.net_.Data;
using Org.BouncyCastle.Crypto.Paddings;

namespace Walking_Dinner__asp.net_.Controllers
{

    public static class DeepCopy
    {
        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }

    public static class Validateemail
    {
        public static bool IsValidEmail(string EmailToCheck)
        {
            try
            {
                MailAddress mail = new MailAddress(EmailToCheck);
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

   

    public class persoondatasController : Controller
        {
      

        public static Random r = new Random();
        public static List<int> maximumlijst = new List<int>();
        public static List<List<string>> personenpergroep = new List<List<string>>();
        public static List<int> cloneablelist = new List<int>();
        public static List<string> Cloneablestringlist = new List<string>();
        public static SortedDictionary<string, int> hostsperpersoon = new SortedDictionary<string, int>();
        public static SortedDictionary<string, int> rondesperpersoon = new SortedDictionary<string, int>();
        public static SortedDictionary<string, int> aantalkeermeegedaanpersoon = new SortedDictionary<string, int>();
        public static List<string> hostpergroep = new List<string>();
        public static SortedDictionary<string, int> cloneablestringkeydictionary = new SortedDictionary<string, int>();
        public static List<string> keuzegerechten = new List<string> { "Aperitief met amuse", "Nagerecht" };
        public static List<string> keuzegerechten2 = new List<string> { "Koud voorgerecht", "Warm voorgerecht" };

        public static List<int> parallelmaximumlijst = new List<int>();
        public static List<int> storenumbers = new List<int> { 1 };
        public static List<List<string>> parallelpersonenpergroep = new List<List<string>>();
        public static SortedDictionary<string, int> parallelhostsperpersoon = new SortedDictionary<string, int>();
        public static SortedDictionary<string, int> parallelrondesperpersoon = new SortedDictionary<string, int>();
        public static SortedDictionary<string, int> parallelaantalkeermeegedaanpersoon = new SortedDictionary<string, int>();
        public static List<string> parallelhostpergroep = new List<string>();
        int x50 = 12;





        private persoonDB db = new persoonDB();
        private  persoonparallelDB dbparallel = new persoonparallelDB();
        private  AdminDB dbadmin = new AdminDB();
        private  AdminparallelDB dbparalleladmin = new AdminparallelDB();
        private adminlogindata adminlogin = new adminlogindata();


        Random rnd = new Random();


        public  class StartupClass
        {
            public void Init()
            {
                maximumlijst.Clear();
                personenpergroep.Clear();
                hostsperpersoon.Clear();
                rondesperpersoon.Clear();
                aantalkeermeegedaanpersoon.Clear();
                hostpergroep.Clear();
                parallelmaximumlijst.Clear();
                parallelpersonenpergroep.Clear();
                parallelhostsperpersoon.Clear();
                parallelrondesperpersoon.Clear();
                parallelaantalkeermeegedaanpersoon.Clear();
                parallelhostpergroep.Clear();
                persoonDB persoonobject = new persoonDB();
                persoonparallelDB persoonparallelobject = new persoonparallelDB();
                AdminDB adminobject = new AdminDB();
                AdminparallelDB adminparallelobject = new AdminparallelDB();


                foreach (var item10 in persoonobject.data)
                {

                    if (!aantalkeermeegedaanpersoon.ContainsKey(item10.emailadress))
                    {
                        aantalkeermeegedaanpersoon.Add(item10.emailadress, item10.aantaalkeermeegedaan);
                    }


                    if (!hostsperpersoon.ContainsKey(item10.emailadress))
                    {
                        hostsperpersoon.Add(item10.emailadress, item10.gastheergeweestaantal);
                    }


                    if (!rondesperpersoon.ContainsKey(item10.emailadress))
                    {
                        rondesperpersoon.Add(item10.emailadress, item10.rondesperpersoon);
                    }
                    
                }
                foreach (var item11 in adminobject.data)
                {
                   
                        maximumlijst.Add(item11.maximum);
                    
          
                        personenpergroep.Add(item11.deelnemers);
              
                        hostpergroep.Add(item11.hostpergroep);
                    

                    
                }

                foreach (var item222 in persoonparallelobject.dataparallel)
                {
                    if (!parallelaantalkeermeegedaanpersoon.ContainsKey(item222.parallelemailadress))
                    {
                        parallelaantalkeermeegedaanpersoon.Add(item222.parallelemailadress, item222.parallelaantaalkeermeegedaan);
                    }


                    if (!parallelhostsperpersoon.ContainsKey(item222.parallelemailadress))
                    {
                        parallelhostsperpersoon.Add(item222.parallelemailadress, item222.parallelgastheergeweestaantal);
                    }


                    if (!parallelrondesperpersoon.ContainsKey(item222.parallelemailadress))
                    {
                        parallelrondesperpersoon.Add(item222.parallelemailadress, item222.parallelrondesperpersoon);
                    }
                    
                }
                foreach (var item111 in adminparallelobject.data)
                {
                  
                        parallelmaximumlijst.Add(item111.parallelmaximum);
                    
                 
                        parallelpersonenpergroep.Add(item111.paralleldeelnemers);
                    
                
                        parallelhostpergroep.Add(item111.parallelhostpergroep);
                 
                }


                // whatever code you need
            }
        }

        // GET: persoondatas
        public ActionResult Adminpage()
        {
             
            return View(dbadmin.data.ToList());
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "id,loginnaam,wachtwoord")] adminlogindata adminlogin)
        {
            if (adminlogin.loginnaam == "adminlogin" && adminlogin.wachtwoord == "adminwachtwoord")
            {
                return View("Adminpage", dbadmin.data.ToList());
            }
            else
            {
                return View();
            }
        }
        public ActionResult startpage()
        {


            return View();
        }
        public ActionResult Index()
        {
            return View(db.data.ToList());
        }

        [HttpPost]
        public ActionResult Index(string agreecheckbox, string submitbutton)
        {
            if (submitbutton == "regular")
            {
                if (agreecheckbox == "check")
                {
                    return View("CreateParallel");
                }
                else
                {
                    return View("Create");
                }
            }
            else
            {
                return View("Login");
            }
        }
        // GET: persoondatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            persoondata persoondata = db.data.Find(id);
            if (persoondata == null)
            {
                return HttpNotFound();
            }
            return View(persoondata);
        }

        // GET: persoondatas/Create
        [HttpGet]
        public ActionResult Pdf()
        {
            PDF.Email(null);
            return RedirectToAction("Create");
        }

        public ActionResult Create()
        {
            return View();
        }



        // POST: persoondatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,volledigenaam,straatnaam,huisnummer,postcode,plaats,telefoonnummer,emailadress,dieetwensen,volledigenaam2,straatnaam2,huisnummer2,postcode2,plaats2,telefoonnummer2,emailadress2,dieetwensen2,groep,isgroepvol,gastheergweest")] persoondata persoondata)
        {

            if (!(storenumbers.First() == 12/ x50))
            {
                if (ModelState.IsValid)
                {



                    if (Validateemail.IsValidEmail(persoondata.emailadress))
                    {
                        int x = 0;
                        int hoevaakingedeeld = 0;
                        if (!aantalkeermeegedaanpersoon.ContainsKey(persoondata.emailadress))
                        {
                            aantalkeermeegedaanpersoon.Add(persoondata.emailadress, 1);
                            persoondata.aantaalkeermeegedaan = 1;
                            db.data.Add(persoondata);
                            db.SaveChanges();


                        }
                        else
                        {
                            aantalkeermeegedaanpersoon[persoondata.emailadress] = aantalkeermeegedaanpersoon[persoondata.emailadress] + 1;
                            var storevalue2 = db.data.Where(x2 => x2.emailadress == persoondata.emailadress).FirstOrDefault();
                            storevalue2.aantaalkeermeegedaan = storevalue2.aantaalkeermeegedaan + 1;
                            db.SaveChanges();

                        }




                        if (!personenpergroep.Any())
                        {

                            x++;
                            personenpergroep.Add(new List<string> { persoondata.emailadress });
                            maximumlijst.Add(r.Next(2, 9));
                            rondesperpersoon.Add(persoondata.emailadress, r.Next(2, 6));

                            persoondata.rondesperpersoon = rondesperpersoon[persoondata.emailadress];
                            db.SaveChanges();
                            admin storevalues4 = new admin { id = 1, maximum = maximumlijst.FirstOrDefault(), deelnemers = personenpergroep[1] };
                            dbadmin.data.Add(storevalues4);
                            db.SaveChanges();

                        }
                        else
                        {
                            List<List<string>> temporarylist = personenpergroep.DeepClone();
                            List<string> storenamestocheck = Cloneablestringlist.DeepClone();
                            List<int> groupswhichareeligable = cloneablelist.DeepClone();
                            List<string> hostnumberpersonsinsidegroup = Cloneablestringlist.DeepClone();
                            List<string> temporaryhostpergroup = hostpergroep.DeepClone();
                            List<string> clonedkeuzegerechten = keuzegerechten.DeepClone();
                            List<string> clonedkeuzegerechten2 = keuzegerechten2.DeepClone();
                            SortedDictionary<string, int> temporarydictionary = cloneablestringkeydictionary.DeepClone();

                            if (!rondesperpersoon.ContainsKey(persoondata.emailadress))
                            {
                                rondesperpersoon.Add(persoondata.emailadress, r.Next(2, 9));
                                persoondata.rondesperpersoon = rondesperpersoon[persoondata.emailadress];
                                db.data.Add(persoondata);
                                db.SaveChanges();

                            }
                            while (x < rondesperpersoon[persoondata.emailadress])
                            {
                                x++;


                                temporaryhostpergroup.Clear();
                                int i = 0;

                                //get index of lists with person + remove person in clone
                                foreach (var index in temporarylist)
                                {
                                    if (index.Contains(persoondata.emailadress))
                                    {
                                        var indexoflist = temporarylist[i].IndexOf(persoondata.emailadress);
                                        temporarylist[i].RemoveAt(indexoflist);
                                        foreach (var item in temporarylist[i])
                                        {

                                            storenamestocheck.Add(item);
                                        }
                                    }
                                    i++;

                                }
                                int xa = 0;
                                //chechk limit is reached of group
                                foreach (int index2 in maximumlijst)
                                {
                                    xa++;
                                    if (maximumlijst[xa - 1] > personenpergroep[xa - 1].Count)
                                    {
                                        if (dbadmin.data.Any(x4 => x4.id == xa))
                                        {
                                            var storevalues7 = dbadmin.data.Where(x4 => x4.id == xa).FirstOrDefault();
                                            storevalues7.maximum = maximumlijst[xa - 1];
                                            storevalues7.deelnemers = personenpergroep[xa - 1];
                                            storevalues7.hostpergroep = "null";
                                        }
                                        else
                                        {
                                            admin storevalues8 = new admin { maximum = r.Next(2, 9), deelnemers = new List<string> { persoondata.emailadress }, hostpergroep = "null" };
                                            dbadmin.data.Add(storevalues8);
                                            db.SaveChanges();
                                        }
                                        if (hostpergroep.Count >= xa)
                                        {
                                            hostpergroep[xa - 1] = ("null");
                                        }
                                        else
                                        {

                                            hostpergroep.Add("null");
                                        }

                                        //check if persons have met eachother already

                                        if (personenpergroep[xa - 1].Intersect(storenamestocheck).Any())
                                        {

                                        }
                                        else
                                        {
                                            groupswhichareeligable.Add(xa - 1);
                                        }

                                    }



                                    else
                                    {
                                        hoevaakingedeeld++;
                                        if (!temporaryhostpergroup.Contains("null"))
                                        {

                                        }
                                        else
                                        {
                                            foreach (var index3 in personenpergroep[xa - 1])
                                            {
                                                hostnumberpersonsinsidegroup.Add(index3);
                                            }


                                            foreach (var index4 in hostnumberpersonsinsidegroup)
                                            {
                                                if (!db.data.Any(x5 => x5.emailadress == index4))
                                                {
                                                    persoondata.gastheergeweestaantal = 0;
                                                    db.data.Add(persoondata);
                                                    db.SaveChanges();
                                                }
                                                if (!hostsperpersoon.ContainsKey(index4))
                                                {
                                                    hostsperpersoon.Add(index4, 0);
                                                    temporarydictionary.Add(index4, 0);
                                                }
                                                else
                                                {
                                                    temporarydictionary.Add(index4, hostsperpersoon[index4]);
                                                }

                                            }
                                            if (x == rondesperpersoon[persoondata.emailadress] && (hostsperpersoon[persoondata.emailadress] < aantalkeermeegedaanpersoon[persoondata.emailadress]))
                                            {
                                                if (x <= hostpergroep.Count)
                                                {
                                                    hostpergroep[x - 1] = persoondata.emailadress;
                                                    hostsperpersoon[persoondata.emailadress] = hostsperpersoon[persoondata.emailadress] + 1;
                                                    var storevalues11 = dbadmin.data.Where(x10 => x10.id == x).FirstOrDefault();
                                                    storevalues11.hostpergroep = persoondata.emailadress;
                                                    dbadmin.SaveChanges();
                                                    var storevalues9 = db.data.Where(x9 => x9.emailadress == persoondata.emailadress).FirstOrDefault();
                                                    storevalues9.gastheergeweestaantal = storevalues9.gastheergeweestaantal + 1;
                                                    db.SaveChanges();
                                                }
                                                else
                                                {
                                                    hostpergroep.Add(persoondata.emailadress);
                                                    hostsperpersoon[persoondata.emailadress] = hostsperpersoon[persoondata.emailadress] + 1;
                                                    admin storevalues12 = new admin { maximum = r.Next(2, 9), deelnemers = new List<string> { persoondata.emailadress }, hostpergroep = "null" };
                                                    dbadmin.data.Add(storevalues12);
                                                    persoondata.gastheergeweestaantal = persoondata.gastheergeweestaantal + 1;
                                                    db.data.Add(persoondata);
                                                }

                                            }
                                            else
                                            {
                                                var indexhost = temporarydictionary.Aggregate((l, a) => l.Value < a.Value ? l : a).Key;

                                                hostpergroep[x - 1] = indexhost;
                                                hostsperpersoon[indexhost] = hostsperpersoon[indexhost] + 1;
                                                var storevalues13 = dbadmin.data.Where(x11 => x11.id == x).FirstOrDefault();
                                                storevalues13.hostpergroep = indexhost;
                                                dbadmin.SaveChanges();
                                                var storevalues14 = db.data.Where(x12 => x12.emailadress == indexhost).FirstOrDefault();
                                                storevalues14.gastheergeweestaantal = storevalues14.gastheergeweestaantal + 1;
                                                db.SaveChanges();


                                            }





                                            DateTime today = DateTime.Today;
                                            int daysUntilTuesday = (((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7 * hoevaakingedeeld) % 7 * hoevaakingedeeld) + 1;
                                            DateTime nextTuesday = today.AddDays(daysUntilTuesday);


                                            var personendieetwensen = personenpergroep[x - 1];
                                            var personendieetwensencloned = personendieetwensen.DeepClone();
                                            List<string> cloneablestringlist = new List<string>();
                                            var clonedstringlist = cloneablestringlist.DeepClone();
                                            string cloneablestring = "";
                                            var clonedstring = cloneablestring.DeepClone();

                                            foreach (var item20 in personendieetwensencloned)
                                            {
                                                clonedstringlist.Add(db.data.Where(x18 => x18.emailadress == item20).FirstOrDefault().dieetwensen);
                                                clonedstringlist.Add(db.data.Where(x18 => x18.emailadress == item20).FirstOrDefault().dieetwensen2);
                                            }

                                            foreach (var item21 in clonedstringlist)
                                            {
                                                clonedstring = clonedstring + "@" + item21;
                                            }

                                            string gethost = dbadmin.data.Where(x22 => x22.id == x).FirstOrDefault().hostpergroep;
                                            string storelocation = (db.data.Where(x23 => x23.emailadress == gethost).FirstOrDefault().straatnaam) + " " + (db.data.Where(x24 => x24.emailadress == gethost).FirstOrDefault().huisnummer);
                                            string gethostmails = (db.data.Where(x45 => x45.emailadress == gethost).FirstOrDefault().emailadress2) + "," + gethost;
                                            var getmailparticipantslist = (dbadmin.data.Where(x46 => x46.id == x).FirstOrDefault());
                                            string mailparticipants = string.Join(",", getmailparticipantslist);


                                            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                                            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("host.pdf", FileMode.Create));
                                            doc.Open();
                                            string storestring1 = "Beste Meneer/Mevrouw @ hierbij bent u gekozen als de gastheer voor de walking dinner op " + nextTuesday.ToString("dd/MM/yyyy") + " om 18:00, @ de gerechten welk u moet voorbereiden bestaan uit het volgende: @ Hoofdgerecht. @" + clonedkeuzegerechten[r.Next(2)] + "@" + clonedkeuzegerechten2[r.Next(2)] + "@" + "dieetwensen zijn het volgende: @" + clonedstring;
                                            string addnewlines1 = storestring1.Replace("@", Environment.NewLine);
                                            Paragraph paragraph = new Paragraph(addnewlines1);
                                            paragraph.IndentationRight = 100;
                                            paragraph.IndentationLeft = 100;
                                            doc.Add(paragraph);
                                            doc.Close();
                                            var pathpdffile = Path.GetFullPath("host.pdf");

                                            Document doc2 = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                                            PdfWriter wri2 = PdfWriter.GetInstance(doc2, new FileStream("bezoeker.pdf", FileMode.Create));
                                            doc2.Open();
                                            // locatie toevoegen!!!!!!!!!
                                            string storestring2 = "Beste Meneer/Mevrouw @ hierbij bent u gekozen als bezoeker voor de walking dinner op " + nextTuesday.ToString("dd/MM/yyyy") + " om 18:00 op " + storelocation;
                                            string addnewlines2 = storestring2.Replace("@", Environment.NewLine);
                                            Paragraph paragraph2 = new Paragraph(addnewlines2);
                                            paragraph2.IndentationRight = 100;
                                            paragraph2.IndentationLeft = 100;
                                            doc2.Add(paragraph2);
                                            doc2.Close();
                                            var pathpdffile2 = Path.GetFullPath("bezoeker.pdf");

                                            byte[] pdfconvertedhost = System.IO.File.ReadAllBytes(pathpdffile);
                                            dbadmin.data.Where(x99 => x99.id == x).FirstOrDefault().preperatordata = pdfconvertedhost;

                                            byte[] pdfconverteddeelnemers = System.IO.File.ReadAllBytes(pathpdffile2);
                                            dbadmin.data.Where(x100 => x100.id == x).FirstOrDefault().bezoekersdata = pdfconverteddeelnemers;

                                            using (MailMessage mail = new MailMessage())
                                            {
                                                mail.From = new MailAddress("testmailopdracht@gmail.com");
                                                foreach (var a1 in gethostmails.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                                                {
                                                    mail.To.Add(a1);
                                                }
                                                mail.Subject = "Walking Dinner";
                                                mail.Body = "Beste Meneer/Mevrouw, in de bijlages bevindt zich de data voor de aankomende Walking Dinner.";
                                                mail.IsBodyHtml = true;
                                                System.Net.Mail.Attachment attachment;
                                                attachment = new System.Net.Mail.Attachment(pathpdffile);
                                                mail.Attachments.Add(attachment);

                                                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                                                {
                                                    smtp.Credentials = new System.Net.NetworkCredential("testmailopdracht@gmail.com", "testmail");
                                                    smtp.EnableSsl = true;
                                                    smtp.Send(mail);
                                                }
                                            }


                                            using (MailMessage mail2 = new MailMessage())
                                            {
                                                mail2.From = new MailAddress("testmailopdracht@gmail.com");
                                                foreach (var a2 in mailparticipants.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                                                {
                                                    mail2.To.Add(a2);
                                                }
                                                mail2.Subject = "Walking Dinner";
                                                mail2.Body = "Beste Meneer/Mevrouw, in de bijlages bevindt zich de data voor de aankomende Walking Dinner.";
                                                mail2.IsBodyHtml = true;
                                                System.Net.Mail.Attachment attachment;
                                                attachment = new System.Net.Mail.Attachment(pathpdffile2);
                                                mail2.Attachments.Add(attachment);

                                                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                                                {
                                                    smtp.Credentials = new System.Net.NetworkCredential("testmailopdracht@gmail.com", "testmail");
                                                    smtp.EnableSsl = true;
                                                    smtp.Send(mail2);
                                                }
                                            }

                                        }

                                    }
                                }

                                if (!groupswhichareeligable.Any())
                                {
                                    personenpergroep.Add(new List<string> { persoondata.emailadress });
                                    admin storevalues62 = new admin { maximum = r.Next(2, 9), deelnemers = new List<string> { persoondata.emailadress }, hostpergroep = "null" };
                                    dbadmin.data.Add(storevalues62);
                                    dbadmin.SaveChanges();
                                    maximumlijst.Add(r.Next(2, 9));

                                }
                                else
                                {
                                    dbadmin.data.Where(x87 => x87.id == groupswhichareeligable.FirstOrDefault()).FirstOrDefault().deelnemers.Add(persoondata.emailadress);
                                    dbadmin.SaveChanges();
                                    personenpergroep[groupswhichareeligable.FirstOrDefault()].Add(persoondata.emailadress);

                                }
                            }
                        }



                        return View("Index");
                    }
                    else
                    {
                        Response.Write(@"<script language='javascript'>alert('Message: \n" + "Voer een geldig e-mailadress in" + "');</script>");


                    }

                }

                return View(persoondata);
            }
            return View("Index");
        }

        public ActionResult CreateParallel()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateParallel([Bind(Include = "id,parallelvolledigenaam,parallelstraatnaam,parallelhuisnummer,parallelpostcode,parallelplaats,paralleltelefoonnummer,parallelemailadress,paralleldieetwensen,parallelvolledigenaam2,parallelstraatnaam2,parallelhuisnummer2,parallelpostcode2,parallelplaats2,paralleltelefoonnummer2,parallelemailadress2,paralleldieetwensen2,parallelgroep,parallelisgroepvol")] persoondataparallel persoondataparallel)
        {
            if (!(storenumbers.First() == 12 / x50))
            {

                if (ModelState.IsValid)
                {

                    if (Validateemail.IsValidEmail(persoondataparallel.parallelemailadress))
                    {
                        int x = 0;
                        int hoevaakingedeeld = 0;
                        if (!parallelaantalkeermeegedaanpersoon.ContainsKey(persoondataparallel.parallelemailadress))
                        {
                            parallelaantalkeermeegedaanpersoon.Add(persoondataparallel.parallelemailadress, 1);
                            persoondataparallel.parallelaantaalkeermeegedaan = 1;
                            dbparallel.dataparallel.Add(persoondataparallel);
                            dbparallel.SaveChanges();


                        }
                        else
                        {
                            parallelaantalkeermeegedaanpersoon[persoondataparallel.parallelemailadress] = parallelaantalkeermeegedaanpersoon[persoondataparallel.parallelemailadress] + 1;
                            var storevalue2 = dbparallel.dataparallel.Where(x2 => x2.parallelemailadress == persoondataparallel.parallelemailadress).FirstOrDefault();
                            storevalue2.parallelaantaalkeermeegedaan = storevalue2.parallelaantaalkeermeegedaan + 1;
                            dbparallel.SaveChanges();

                        }


                        if (!parallelpersonenpergroep.Any())
                        {

                            x++;
                            parallelpersonenpergroep.Add(new List<string> { persoondataparallel.parallelemailadress });
                            parallelmaximumlijst.Add(r.Next(2, 9));
                            parallelrondesperpersoon.Add(persoondataparallel.parallelemailadress, r.Next(2, 6));

                            persoondataparallel.parallelrondesperpersoon = parallelrondesperpersoon[persoondataparallel.parallelemailadress];
                            dbparallel.SaveChanges();
                            adminparallel storevalues4 = new adminparallel { id = 1, parallelmaximum = parallelmaximumlijst.FirstOrDefault(), paralleldeelnemers = parallelpersonenpergroep[1] };
                            dbparalleladmin.data.Add(storevalues4);
                            dbparallel.SaveChanges();

                        }
                        else
                        {
                            List<List<string>> temporarylist = parallelpersonenpergroep.DeepClone();
                            List<string> storenamestocheck = Cloneablestringlist.DeepClone();
                            List<int> groupswhichareeligable = cloneablelist.DeepClone();
                            List<string> hostnumberpersonsinsidegroup = Cloneablestringlist.DeepClone();
                            List<string> temporaryhostpergroup = parallelhostpergroep.DeepClone();
                            List<string> clonedkeuzegerechten = keuzegerechten.DeepClone();
                            List<string> clonedkeuzegerechten2 = keuzegerechten2.DeepClone();
                            SortedDictionary<string, int> temporarydictionary = cloneablestringkeydictionary.DeepClone();

                            if (!parallelrondesperpersoon.ContainsKey(persoondataparallel.parallelemailadress))
                            {
                                parallelrondesperpersoon.Add(persoondataparallel.parallelemailadress, r.Next(2, 9));
                                persoondataparallel.parallelrondesperpersoon = parallelrondesperpersoon[persoondataparallel.parallelemailadress];
                                dbparallel.dataparallel.Add(persoondataparallel);
                                dbparallel.SaveChanges();

                            }
                            while (x < parallelrondesperpersoon[persoondataparallel.parallelemailadress])
                            {
                                x++;


                                temporaryhostpergroup.Clear();
                                int i = 0;

                                //get index of lists with person + remove person in clone
                                foreach (var index in temporarylist)
                                {
                                    if (index.Contains(persoondataparallel.parallelemailadress))
                                    {
                                        var indexoflist = temporarylist[i].IndexOf(persoondataparallel.parallelemailadress);
                                        temporarylist[i].RemoveAt(indexoflist);
                                        foreach (var item in temporarylist[i])
                                        {

                                            storenamestocheck.Add(item);
                                        }
                                    }
                                    i++;

                                }
                                int xa = 0;
                                //chechk limit is reached of group
                                foreach (int index2 in parallelmaximumlijst)
                                {
                                    xa++;
                                    if (parallelmaximumlijst[xa - 1] > parallelpersonenpergroep[xa - 1].Count)
                                    {
                                        if (dbparalleladmin.data.Any(x4 => x4.id == xa))
                                        {
                                            var storevalues7 = dbparalleladmin.data.Where(x4 => x4.id == xa).FirstOrDefault();
                                            storevalues7.parallelmaximum = parallelmaximumlijst[xa - 1];
                                            storevalues7.paralleldeelnemers = parallelpersonenpergroep[xa - 1];
                                            storevalues7.parallelhostpergroep = "null";
                                        }
                                        else
                                        {
                                            adminparallel storevalues8 = new adminparallel { parallelmaximum = r.Next(2, 9), paralleldeelnemers = new List<string> { persoondataparallel.parallelemailadress }, parallelhostpergroep = "null" };
                                            dbparalleladmin.data.Add(storevalues8);
                                            dbparallel.SaveChanges();
                                        }
                                        if (parallelhostpergroep.Count >= xa)
                                        {
                                            parallelhostpergroep[xa - 1] = ("null");
                                        }
                                        else
                                        {

                                            parallelhostpergroep.Add("null");
                                        }

                                        //check if persons have met eachother already

                                        if (parallelpersonenpergroep[xa - 1].Intersect(storenamestocheck).Any())
                                        {

                                        }
                                        else
                                        {
                                            groupswhichareeligable.Add(xa - 1);
                                        }

                                    }



                                    else
                                    {
                                        hoevaakingedeeld++;
                                        if (!temporaryhostpergroup.Contains("null"))
                                        {

                                        }
                                        else
                                        {
                                            foreach (var index3 in parallelpersonenpergroep[xa - 1])
                                            {
                                                hostnumberpersonsinsidegroup.Add(index3);
                                            }


                                            foreach (var index4 in hostnumberpersonsinsidegroup)
                                            {
                                                if (!dbparallel.dataparallel.Any(x5 => x5.parallelemailadress == index4))
                                                {
                                                    persoondataparallel.parallelgastheergeweestaantal = 0;
                                                    dbparallel.dataparallel.Add(persoondataparallel);
                                                    dbparallel.SaveChanges();
                                                }
                                                if (!parallelhostsperpersoon.ContainsKey(index4))
                                                {
                                                    parallelhostsperpersoon.Add(index4, 0);
                                                    temporarydictionary.Add(index4, 0);
                                                }
                                                else
                                                {
                                                    temporarydictionary.Add(index4, parallelhostsperpersoon[index4]);
                                                }

                                            }
                                            if (x == parallelrondesperpersoon[persoondataparallel.parallelemailadress] && (parallelhostsperpersoon[persoondataparallel.parallelemailadress] < parallelaantalkeermeegedaanpersoon[persoondataparallel.parallelemailadress]))
                                            {
                                                if (x <= parallelhostpergroep.Count)
                                                {
                                                    parallelhostpergroep[x - 1] = persoondataparallel.parallelemailadress;
                                                    parallelhostsperpersoon[persoondataparallel.parallelemailadress] = parallelhostsperpersoon[persoondataparallel.parallelemailadress] + 1;
                                                    var storevalues11 = dbparalleladmin.data.Where(x10 => x10.id == x).FirstOrDefault();
                                                    storevalues11.parallelhostpergroep = persoondataparallel.parallelemailadress;
                                                    dbparalleladmin.SaveChanges();
                                                    var storevalues9 = dbparallel.dataparallel.Where(x9 => x9.parallelemailadress == persoondataparallel.parallelemailadress).FirstOrDefault();
                                                    storevalues9.parallelgastheergeweestaantal = storevalues9.parallelgastheergeweestaantal + 1;
                                                    dbparallel.SaveChanges();
                                                }
                                                else
                                                {
                                                    parallelhostpergroep.Add(persoondataparallel.parallelemailadress);
                                                    parallelhostsperpersoon[persoondataparallel.parallelemailadress] = parallelhostsperpersoon[persoondataparallel.parallelemailadress] + 1;
                                                    adminparallel storevalues12 = new adminparallel { parallelmaximum = r.Next(2, 9), paralleldeelnemers = new List<string> { persoondataparallel.parallelemailadress }, parallelhostpergroep = "null" };
                                                    dbparalleladmin.data.Add(storevalues12);
                                                    persoondataparallel.parallelgastheergeweestaantal = persoondataparallel.parallelgastheergeweestaantal + 1;
                                                    dbparallel.dataparallel.Add(persoondataparallel);
                                                }

                                            }
                                            else
                                            {
                                                var indexhost = temporarydictionary.Aggregate((l, a) => l.Value < a.Value ? l : a).Key;

                                                parallelhostpergroep[x - 1] = indexhost;
                                                parallelhostsperpersoon[indexhost] = parallelhostsperpersoon[indexhost] + 1;
                                                var storevalues13 = dbparalleladmin.data.Where(x11 => x11.id == x).FirstOrDefault();
                                                storevalues13.parallelhostpergroep = indexhost;
                                                dbparalleladmin.SaveChanges();
                                                var storevalues14 = dbparallel.dataparallel.Where(x12 => x12.parallelemailadress == indexhost).FirstOrDefault();
                                                storevalues14.parallelgastheergeweestaantal = storevalues14.parallelgastheergeweestaantal + 1;
                                                dbparallel.SaveChanges();


                                            }





                                            DateTime today = DateTime.Today;
                                            int daysUntilTuesday = (((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7 * hoevaakingedeeld) % 7 * hoevaakingedeeld) + 1;
                                            DateTime nextTuesday = today.AddDays(daysUntilTuesday);


                                            var personendieetwensen = parallelpersonenpergroep[x - 1];
                                            var personendieetwensencloned = personendieetwensen.DeepClone();
                                            List<string> cloneablestringlist = new List<string>();
                                            var clonedstringlist = cloneablestringlist.DeepClone();
                                            string cloneablestring = "";
                                            var clonedstring = cloneablestring.DeepClone();

                                            foreach (var item20 in personendieetwensencloned)
                                            {
                                                clonedstringlist.Add(dbparallel.dataparallel.Where(x18 => x18.parallelemailadress == item20).FirstOrDefault().paralleldieetwensen);
                                                clonedstringlist.Add(dbparallel.dataparallel.Where(x18 => x18.parallelemailadress == item20).FirstOrDefault().paralleldieetwensen2);
                                            }

                                            foreach (var item21 in clonedstringlist)
                                            {
                                                clonedstring = clonedstring + "@" + item21;
                                            }


                                            string gethost = dbparalleladmin.data.Where(x22 => x22.id == x).FirstOrDefault().parallelhostpergroep;
                                            string storelocation = (dbparallel.dataparallel.Where(x23 => x23.parallelemailadress == gethost).FirstOrDefault().parallelstraatnaam) + (dbparallel.dataparallel.Where(x24 => x24.parallelemailadress == gethost).FirstOrDefault().parallelhuisnummer);
                                            string gethostmails = (dbparallel.dataparallel.Where(x45 => x45.parallelemailadress == gethost).FirstOrDefault().parallelemailadress2) + "," + gethost;
                                            var getmailparticipantslist = (dbparalleladmin.data.Where(x46 => x46.id == x).FirstOrDefault());
                                            string mailparticipants = string.Join(",", getmailparticipantslist);


                                            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                                            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("host.pdf", FileMode.Create));
                                            doc.Open();
                                            string storestring1 = "Beste Meneer/Mevrouw @ hierbij bent u gekozen als de gastheer voor de walking dinner op " + nextTuesday.ToString("dd/MM/yyyy") + " om 18:00, @ de gerechten welk u moet voorbereiden bestaan uit het volgende: @ Hoofdgerecht. @" + clonedkeuzegerechten[r.Next(2)] + "@" + clonedkeuzegerechten2[r.Next(2)] + "@" + "dieetwensen zijn het volgende: @" + clonedstring;
                                            string addnewlines1 = storestring1.Replace("@", Environment.NewLine);
                                            Paragraph paragraph = new Paragraph(addnewlines1);
                                            paragraph.IndentationRight = 100;
                                            paragraph.IndentationLeft = 100;
                                            doc.Add(paragraph);
                                            doc.Close();
                                            var pathpdffile = Path.GetFullPath("host.pdf");

                                            Document doc2 = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                                            PdfWriter wri2 = PdfWriter.GetInstance(doc2, new FileStream("bezoeker.pdf", FileMode.Create));
                                            doc2.Open();
                                            // locatie toevoegen!!!!!!!!!
                                            string storestring2 = "Beste Meneer/Mevrouw @ hierbij bent u gekozen als bezoeker voor de walking dinner op " + nextTuesday.ToString("dd/MM/yyyy") + " om 18:00 op " + storelocation;
                                            string addnewlines2 = storestring2.Replace("@", Environment.NewLine);
                                            Paragraph paragraph2 = new Paragraph(addnewlines2);
                                            paragraph2.IndentationRight = 100;
                                            paragraph2.IndentationLeft = 100;
                                            doc2.Add(paragraph2);
                                            doc2.Close();
                                            var pathpdffile2 = Path.GetFullPath("bezoeker.pdf");
                                            byte[] pdfconvertedhost2 = System.IO.File.ReadAllBytes(pathpdffile);
                                            dbparalleladmin.data.Where(x99 => x99.id == x).FirstOrDefault().parallelpreperatordata = pdfconvertedhost2;

                                            byte[] pdfconverteddeelnemers2 = System.IO.File.ReadAllBytes(pathpdffile2);
                                            dbparalleladmin.data.Where(x100 => x100.id == x).FirstOrDefault().parallelbezoekersdata = pdfconverteddeelnemers2;

                                            using (MailMessage mail = new MailMessage())
                                            {
                                                mail.From = new MailAddress("testmailopdracht@gmail.com");
                                                foreach (var a1 in mailparticipants.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                                                {
                                                    mail.To.Add(a1);
                                                }
                                                mail.Subject = "Walking Dinner";
                                                mail.Body = "Beste Meneer/Mevrouw, in de bijlages bevindt zich de data voor de aankomende Walking Dinner.";
                                                mail.IsBodyHtml = true;
                                                System.Net.Mail.Attachment attachment;
                                                attachment = new System.Net.Mail.Attachment(pathpdffile);
                                                mail.Attachments.Add(attachment);

                                                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                                                {
                                                    smtp.Credentials = new System.Net.NetworkCredential("testmailopdracht@gmail.com", "testmail");
                                                    smtp.EnableSsl = true;
                                                    smtp.Send(mail);
                                                }
                                            }


                                            using (MailMessage mail2 = new MailMessage())
                                            {
                                                mail2.From = new MailAddress("testmailopdracht@gmail.com");
                                                foreach (var a2 in mailparticipants.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                                                {
                                                    mail2.To.Add(a2);
                                                }
                                                mail2.Subject = "Walking Dinner";
                                                mail2.Body = "Beste Meneer/Mevrouw, in de bijlages bevindt zich de data voor de aankomende Walking Dinner.";
                                                mail2.IsBodyHtml = true;
                                                System.Net.Mail.Attachment attachment;
                                                attachment = new System.Net.Mail.Attachment(pathpdffile2);
                                                mail2.Attachments.Add(attachment);

                                                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                                                {
                                                    smtp.Credentials = new System.Net.NetworkCredential("testmailopdracht@gmail.com", "testmail");
                                                    smtp.EnableSsl = true;
                                                    smtp.Send(mail2);
                                                }
                                            }

                                        }

                                    }
                                }

                                if (!groupswhichareeligable.Any())
                                {
                                    parallelpersonenpergroep.Add(new List<string> { persoondataparallel.parallelemailadress });
                                    adminparallel storevalues62 = new adminparallel { parallelmaximum = r.Next(2, 9), paralleldeelnemers = new List<string> { persoondataparallel.parallelemailadress }, parallelhostpergroep = "null" };
                                    dbparalleladmin.data.Add(storevalues62);
                                    dbparalleladmin.SaveChanges();
                                    parallelmaximumlijst.Add(r.Next(2, 9));

                                }
                                else
                                {
                                    dbparalleladmin.data.Where(x87 => x87.id == groupswhichareeligable.FirstOrDefault()).FirstOrDefault().paralleldeelnemers.Add(persoondataparallel.parallelemailadress);
                                    dbparalleladmin.SaveChanges();
                                    parallelpersonenpergroep[groupswhichareeligable.FirstOrDefault()].Add(persoondataparallel.parallelemailadress);

                                }
                            }
                        }



                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Response.Write(@"<script language='javascript'>alert('Message: \n" + "Voer een geldig e-mailadress in" + "');</script>");


                    }

                }

                return View(persoondataparallel);
            }
            return View("Index");
        }




   
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
