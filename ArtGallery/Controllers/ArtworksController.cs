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
    public class ArtworksController : Controller
    {
        private ArtworkDbContext db = new ArtworkDbContext();

        // GET: Artworks
        public ActionResult Index()
        {
            var r = (from foo in db.Artworks
                     select foo);

            var q = (from aws in db.Artworks
                     join ar in db.Artists on aws.ArtistId equals ar.ArtistId
                     join p in db.Pieces on aws.ArtworkId equals p.ArtworkId
                     where aws.NumberInInventory > 1
                     select new { aws.Title, aws.NumberInInventory, ar.Name, p.ImageURL, aws.Category, aws.Medium, aws.ArtworkId });

            var qq = q.AsEnumerable().Select(xx => new ArtworkArtistPieceViewModel
            {
                Title = xx.Title,
                NumberInInventory = xx.NumberInInventory,
                Name = xx.Name,
                ImageURL = xx.ImageURL,
                Category = xx.Category,
                Medium = xx.Medium,
                ArtworkId = xx.ArtworkId
            }).ToList();

            return View(qq);

            //return View(db.Artworks.Where(a => a.NumberInInventory > 0).ToList());
        }

        // GET: Artworks/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artwork artwork = db.Artworks.Find(id);
            if (artwork == null)
            {
                return HttpNotFound();
            }
            return View(artwork);
        }

        // GET: Artworks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artworks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtworkId,ArtistId,Title,YearOriginalCreated,Medium,Dimensions,NumberMade,NumberInInventory,NumberSold")] Artwork artwork)
        {
            if (ModelState.IsValid)
            {
                db.Artworks.Add(artwork);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artwork);
        }

        // GET: Artworks/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artwork artwork = db.Artworks.Find(id);
            if (artwork == null)
            {
                return HttpNotFound();
            }
            return View(artwork);
        }

        // POST: Artworks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtworkId,ArtistId,Title,YearOriginalCreated,Medium,Dimensions,NumberMade,NumberInInventory,NumberSold")] Artwork artwork)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artwork).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artwork);
        }

        // GET: Artworks/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artwork artwork = db.Artworks.Find(id);
            if (artwork == null)
            {
                return HttpNotFound();
            }
            return View(artwork);
        }

        // POST: Artworks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Artwork artwork = db.Artworks.Find(id);
            db.Artworks.Remove(artwork);
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
