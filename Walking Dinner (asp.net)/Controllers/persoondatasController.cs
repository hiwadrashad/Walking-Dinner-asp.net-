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

        private static persoonDB db = new persoonDB();
        private static persoonparallelDB dbparallel = new persoonparallelDB();
        private static AdminDB dbadmin = new AdminDB();
        private static AdminparallelDB dbparalleladmin = new AdminparallelDB();

        Random rnd = new Random();


        public static class StartupClass
        {
            public static void Init()
            {
                maximumlijst.Clear();
                personenpergroep.Clear();
                hostsperpersoon.Clear();
                rondesperpersoon.Clear();
                aantalkeermeegedaanpersoon.Clear();
                hostpergroep.Clear();
                foreach (var item10 in db.data)
                {
                    if (aantalkeermeegedaanpersoon.ContainsKey(item10.emailadress))
                    {
                        aantalkeermeegedaanpersoon[item10.emailadress] = item10.aantaalkeermeegedaan;
                    }
                    else
                    {
                        aantalkeermeegedaanpersoon.Add(item10.emailadress, item10.aantaalkeermeegedaan);
                    }

                    if (hostsperpersoon.ContainsKey(item10.emailadress))
                    {
                        aantalkeermeegedaanpersoon[item10.emailadress] = item10.gastheergeweestaantal;
                    }
                    else
                    {
                        aantalkeermeegedaanpersoon.Add(item10.emailadress, item10.gastheergeweestaantal);
                    }

                    if (rondesperpersoon.ContainsKey(item10.emailadress))
                    {
                        aantalkeermeegedaanpersoon[item10.emailadress] = item10.rondesperpersoon;
                    }
                    else
                    {
                        aantalkeermeegedaanpersoon.Add(item10.emailadress, item10.rondesperpersoon);
                    }
                }
                foreach (var item11 in dbadmin.data)
                {
                    maximumlijst.Add(item11.maximum);
                    personenpergroep.Add(item11.deelnemers);
                    hostpergroep.Add(item11.hostpergroep);
                }

                // whatever code you need
            }
        }

        // GET: persoondatas
        public ActionResult Adminpage()
        {
            return View(dbadmin.data.ToList());
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
        public ActionResult Index(string agreecheckbox)
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
                    var storevalue2 = db.data.Where(x2 => x2.emailadress == persoondata.emailadress).First();
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
                    admin storevalues4 = new admin { id = 1, maximum = maximumlijst.First(), deelnemers = personenpergroep[1] };
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
                                    var storevalues7 = dbadmin.data.Where(x4 => x4.id == xa).First();
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
                                            var storevalues11 = dbadmin.data.Where(x10 => x10.id == x).First();
                                            storevalues11.hostpergroep = persoondata.emailadress;
                                            dbadmin.SaveChanges();
                                            var storevalues9 = db.data.Where(x9 => x9.emailadress == persoondata.emailadress).First();
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
                                        var storevalues13 = dbadmin.data.Where(x11 => x11.id == x).First();
                                        storevalues13.hostpergroep = indexhost;
                                        dbadmin.SaveChanges();
                                        var storevalues14 = db.data.Where(x12 => x12.emailadress == indexhost).First();
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
                                        clonedstringlist.Add(db.data.Where(x18 => x18.emailadress == item20).First().dieetwensen);
                                        clonedstringlist.Add(db.data.Where(x18 => x18.emailadress == item20).First().dieetwensen2);
                                    }

                                    foreach (var item21 in clonedstringlist)
                                    {
                                        clonedstring = clonedstring + "@" + item21;
                                    }

                                    string gethost = dbadmin.data.Where(x22 => x22.id == x).First().hostpergroep;
                                    string storelocation = (db.data.Where(x23 => x23.emailadress == gethost).First().straatnaam) + (db.data.Where(x24 => x24.emailadress == gethost).First().huisnummer);


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

                                    using (MailMessage mail = new MailMessage())
                                    {
                                        mail.From = new MailAddress("testmailopdracht@gmail.com");
                                        mail.To.Add("hiwadrashad1@gmail.com");
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
                                        mail2.To.Add("hiwadrashad1@gmail.com");
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
                            dbadmin.data.Where(x87 => x87.id == groupswhichareeligable.First()).First().deelnemers.Add(persoondata.emailadress);
                            dbadmin.SaveChanges();
                            personenpergroep[groupswhichareeligable.First()].Add(persoondata.emailadress);

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

            return View(persoondata);
        }

        public ActionResult CreateParallel()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateParallel([Bind(Include = "id,parallelvolledigenaam,parallelstraatnaam,parallelhuisnummer,parallelpostcode,parallelplaats,paralleltelefoonnummer,parallelemailadress,paralleldieetwensen,parallelvolledigenaam2,parallelstraatnaam2,parallelhuisnummer2,parallelpostcode2,parallelplaats2,paralleltelefoonnummer2,parallelemailadress2,paralleldieetwensen2,parallelgroep,parallelisgroepvol")] persoondataparallel persoondataparallel)
        {
            if (ModelState.IsValid)
            {               
                dbparallel.dataparallel.Add(persoondataparallel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(persoondataparallel);
        }
        // GET: persoondatas/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: persoondatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,volledigenaam,straatnaam,huisnummer,postcode,plaats,telefoonnummer,emailadress,dieetwensen,volledigenaam2,straatnaam2,huisnummer2,postcode2,plaats2,telefoonnummer2,emailadress2,dieetwensen2,groep")] persoondata persoondata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persoondata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(persoondata);
        }

        // GET: persoondatas/Delete/5
        public ActionResult Delete(int? id)
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


        // POST: persoondatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
        
            persoondata persoondata = db.data.Find(id);
            db.data.Remove(persoondata);
            db.SaveChanges();
            return RedirectToAction("Index");
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
