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
using Walking_Dinner__asp.net_.Documents;
using Walking_Dinner__asp.net_.Data;

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
        public class persoondatasController : Controller
    {

        public persoondatasController()
        {
            dataset = new DataSet();
            dataset.Tables.AddRange(new[]{
            DbStub.GenerateDataTable<persoondata>(50),
            DbStub.GenerateDataTable<persoondataparallel>(50)});
        }


        private DataSet dataset;
        private persoonDB db = new persoonDB();

        private persoonparallelDB dbparallel = new persoonparallelDB();
        Random rnd = new Random();

        // GET: persoondatas

        public ActionResult startpage()
        {


            return View();
        }
        public ActionResult Index()
        {
            return View(dataset.Tables[0].AsEnumerable());

            //return View(db.data.ToList());

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

        [HttpGet]
        public ActionResult Pdf()
        {
            PDF.Create(null);
            return RedirectToAction("Create");
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
                db.data.Add(persoondata);
                db.SaveChanges();
                return RedirectToAction("Index");
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
