using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtGallery.Models;
using System.Globalization;

namespace ArtGallery.Controllers
{
    public class ArtworksController : Controller
    {
        private ArtworkDbContext db = new ArtworkDbContext();

        // GET: Artworks
        public ActionResult Index(string artistString, string mediumString, string categoryString, string priceString)
        {

            var ArtistQry = from a in db.Artists
                           orderby a.Name
                           select a.Name;

            var ArtistList = new List<string>();
            ArtistList.AddRange(ArtistQry.Distinct());
            ViewData["artistString"] = new SelectList(ArtistList);



            var MediumQry = from a in db.Artworkz
                            orderby a.Title
                            select a.Medium;

            var MediumList = new List<string>();
            MediumList.AddRange(MediumQry.Distinct());
            ViewData["mediumString"] = new SelectList(MediumList);



            var CategoryQry = from a in db.Artworkz
                            orderby a.Category
                            select a.Category;

            var CategoryList = new List<string>();
            CategoryList.AddRange(CategoryQry.Distinct());
            ViewData["categoryString"] = new SelectList(CategoryList);



            var PriceList = new List<string>();
            PriceList.Add("0 - $100");
            PriceList.Add("$101 - $250");
            PriceList.Add("$251 - $500");
            PriceList.Add("$501 - $1000");
            PriceList.Add("> $1000");
            ViewData["priceString"] = new SelectList(PriceList);




            var q = (from aws in db.Artworkz
                     join ar in db.Artists on aws.ArtistId equals ar.ArtistId
                     join p in db.Pieces on aws.ArtworkId equals p.ArtworkId
                     where aws.NumberInInventory > 0
                     select new ArtworkArtistPieceViewModel
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

            // q is assigned type IQueryable<'a> (anonymous)
            //var qq = q.AsEnumerable().Select(xx => new ArtworkArtistPieceViewModel  // use this syntax when retrieving > 1 item
            //{
            //    Title = xx.Title,
            //    NumberInInventory = xx.NumberInInventory,
            //    Name = xx.Name,
            //    ImageURL = xx.ImageURL,
            //    Category = xx.Category,
            //    Medium = xx.Medium,
            //    ArtworkId = xx.ArtworkId,
            //    Dimensions = xx.Dimensions,
            //    Location = xx.Location,
            //    Price = xx.Price
            //}).ToList();

            if (!string.IsNullOrEmpty(artistString))
            {
                q = q.Where(s => s.Name.Contains(artistString));
            }

            if (!string.IsNullOrEmpty(mediumString))
            {
                q = q.Where(x => x.Medium == mediumString);
            }
            if (!string.IsNullOrEmpty(categoryString))
            {
                q = q.Where(x => x.Category == categoryString);
            }

            switch (priceString)
            {
                case "0 - $100":
                    var z = new List<ArtworkArtistPieceViewModel>();
                    foreach (ArtworkArtistPieceViewModel piece in q)
                    {
                        if (int.Parse(piece.Price, NumberStyles.Currency) < 101)
                        {
                            z.Add(piece);
                        }
                    }
                    q = z.AsQueryable();
                    break;
                case "$101 - $250":
                    //var zz = new List<ArtworkArtistPieceViewModel>();
                    //foreach (ArtworkArtistPieceViewModel piece in q)
                    //{
                    //    if (int.Parse(piece.Price, NumberStyles.Currency) > 100 && int.Parse(piece.Price, NumberStyles.Currency) < 251)
                    //    {
                    //        zz.Add(piece);
                    //    }
                    //}
                    //q = zz.AsQueryable();
                    q = q.Where(x => Convert.ToInt32(x.Price) > 100 && Convert.ToInt32(x.Price) < 250);
                    break;
                case "$251 - $500":
                    var zzz = new List<ArtworkArtistPieceViewModel>();
                    foreach (ArtworkArtistPieceViewModel piece in q)
                    {
                        if (int.Parse(piece.Price, NumberStyles.Currency) > 250 && int.Parse(piece.Price, NumberStyles.Currency) < 500)
                        {
                            zzz.Add(piece);
                        }
                    }
                    q = zzz.AsQueryable();
                    break;
                case "$501 - $1000":
                    var zzzz = new List<ArtworkArtistPieceViewModel>();
                    foreach (ArtworkArtistPieceViewModel piece in q)
                    {
                        if (int.Parse(piece.Price, NumberStyles.Currency) > 500 && int.Parse(piece.Price, NumberStyles.Currency) < 1000)
                        {
                            zzzz.Add(piece);
                        }
                    }
                    q = zzzz.AsQueryable();
                    break;
                case "> $1000":
                    var zzzzz = new List<ArtworkArtistPieceViewModel>();
                    foreach (ArtworkArtistPieceViewModel piece in q)
                    {
                        if (int.Parse(piece.Price, NumberStyles.Currency) > 1000)
                        {
                            zzzzz.Add(piece);
                        }
                    }
                    q = zzzzz.AsQueryable();
                    break;
                default:
                    break;                     
            }


            //if (priceString == "0 - $100")
            //{
            //    q = q.Where(x => decimal.Parse(x.Price, NumberStyles.Currency) < 101);
            //}
            //if (priceString == "$101 - $250")
            //{
            //    q = q.Where(x => decimal.Parse(x.Price, NumberStyles.Currency) > 100 && decimal.Parse(x.Price, NumberStyles.Currency) < 250);
            //}
            //if (priceString == "$251 - $500")
            //{
            //    q = q.Where(x => decimal.Parse(x.Price, NumberStyles.Currency) > 250 && decimal.Parse(x.Price, NumberStyles.Currency) < 501);
            //}
            //if (priceString == "$251 - $500")
            //{
            //    q = q.Where(x => decimal.Parse(x.Price, NumberStyles.Currency) > 250 && decimal.Parse(x.Price, NumberStyles.Currency) < 501);
            //}

            return View(q);  // qq is type List<ArtworkArtistPriceViewModel>
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
