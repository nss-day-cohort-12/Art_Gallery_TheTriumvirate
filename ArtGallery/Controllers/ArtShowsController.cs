using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtGallery.Models;

namespace ArtGallery.Controllers
{
    public class ArtShowsController : Controller
    {
        private ArtGalleryDbContext db = new ArtGalleryDbContext();

        // GET: ArtShows
        public ActionResult Index()
        {
            var q = (from a in db.ArtShows
                     orderby a.ArtShowId descending
                     select new ArtShowVM
                     {
                         ArtistsRepresented = a.ArtistsRepresented,
                         Name = a.Name,
                         Date = a.Date,
                         ShowLocation = a.ShowLocation,
                         Agents = a.Agents,
                         Overhead = a.Overhead
                     });

            return View(q);
        }

        // GET: ArtShows/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtShow artShow = db.ArtShows.Find(id);
            if (artShow == null)
            {
                return HttpNotFound();
            }
            return View(artShow);
        }

        // GET: ArtShows/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtShows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtShowId,ArtistsRepresented,ShowLocation,Agents,Overhead")] ArtShow artShow)
        {
            if (ModelState.IsValid)
            {
                db.ArtShows.Add(artShow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artShow);
        }

        // GET: ArtShows/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtShow artShow = db.ArtShows.Find(id);
            if (artShow == null)
            {
                return HttpNotFound();
            }
            return View(artShow);
        }

        // POST: ArtShows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtShowId,ArtistsRepresented,ShowLocation,Agents,Overhead")] ArtShow artShow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artShow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artShow);
        }

        // GET: ArtShows/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtShow artShow = db.ArtShows.Find(id);
            if (artShow == null)
            {
                return HttpNotFound();
            }
            return View(artShow);
        }

        // POST: ArtShows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            ArtShow artShow = db.ArtShows.Find(id);
            db.ArtShows.Remove(artShow);
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
