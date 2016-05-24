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

            var q = (from aws in db.Artworkz
                     join ar in db.Artists on aws.ArtistId equals ar.ArtistId
                     join p in db.Pieces on aws.ArtworkId equals p.ArtworkId
                     where aws.NumberInInventory > 0
                     select new { aws.Title, aws.NumberInInventory, ar.Name, p.ImageURL, aws.Category, aws.Medium, aws.ArtworkId,
                                  aws.Dimensions, p.Location, p.Price});

            // q is assigned type IQueryable<'a> (anonymous)
            List<ArtworkArtistPieceViewModel> qq = q.AsEnumerable().Select(xx => new ArtworkArtistPieceViewModel  // use this syntax when retrieving > 1 item
            {
                Title = xx.Title,
                NumberInInventory = xx.NumberInInventory,
                Name = xx.Name,
                ImageURL = xx.ImageURL,
                Category = xx.Category,
                Medium = xx.Medium,
                ArtworkId = xx.ArtworkId,
                Dimensions = xx.Dimensions,
                Location = xx.Location,
                Price = xx.Price
            }).ToList();

            return View(qq);  // qq is type List<ArtworkArtistPriceViewModel>
        }

        // GET: Artworks/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var q = (from aws in db.Artworkz
                     join ar in db.Artists on aws.ArtistId equals ar.ArtistId
                     join p in db.Pieces on aws.ArtworkId equals p.ArtworkId
                     where aws.ArtworkId == id
                     select new ArtworkArtistPieceViewModel // this works because we are only retrieving one item
                     {
                         Title = aws.Title,
                         NumberInInventory = aws.NumberInInventory,
                         Name = ar.Name,
                         ImageURL = p.ImageURL,
                         Category = aws.Category,
                         Medium = aws.Medium,
                         ArtworkId = aws.ArtworkId,
                         Dimensions = aws.Dimensions,
                         Location = p.Location,
                         Price = p.Price
                     });

            // ** LINQ always returns a sequence, so you have to retrieve the item out of it. **
            // There are four LINQ methods to retrieve a single item out of a sequence:
            // Single() returns the item, throws an exception if there are 0 or more than one item in the sequence.
            // SingleOrDefault() returns the item, or default value (null for string). Throws if more than one item in the sequence.
            // First() returns the first item. Throws if there are 0 items in the sequence.
            // FirstOrDefault() returns the first item, or the default value if there are no items).
            return View(q.Single());
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
                db.Artworkz.Add(artwork);
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
            Artwork artwork = db.Artworkz.Find(id);
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
            Artwork artwork = db.Artworkz.Find(id);
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
            Artwork artwork = db.Artworkz.Find(id);
            db.Artworkz.Remove(artwork);
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
