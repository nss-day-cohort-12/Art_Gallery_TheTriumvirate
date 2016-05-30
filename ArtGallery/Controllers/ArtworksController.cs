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
using System.Text.RegularExpressions;

namespace ArtGallery.Controllers
{
    public class ArtworksController : Controller
    {
        private ArtGalleryDbContext db = new ArtGalleryDbContext();

        // GET: Artworks
        public ActionResult Gallery(string artistString, string mediumString, string categoryString, string priceString)
        {
            //Pulls all the artist names from the Artist table
            var ArtistQry = from a in db.Artists
                           orderby a.Name
                           select a.Name;

            //Creates a list of names to populate a select element
            var ArtistList = new List<string>();
            //Adds a collection to the list, adding each unique string only once
            ArtistList.AddRange(ArtistQry.Distinct());
            //Binds the variable string to the elect list?
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
                     }).Distinct();

            //If one of the bound parameters receives a value from the drop down menus, the value of q will be changed/filtered
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

            //Same function as the if statements above, the switch just seemed more appropriate for multiple conditions on one variable
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
                    var zz = new List<ArtworkArtistPieceViewModel>();
                    foreach (ArtworkArtistPieceViewModel piece in q)
                    {
                        if (int.Parse(piece.Price, NumberStyles.Currency) > 100 && int.Parse(piece.Price, NumberStyles.Currency) < 251)
                        {
                            zz.Add(piece);
                        }
                    }
                    q = zz.AsQueryable();
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


            return View(q);  // q is type List<ArtworkArtistPriceViewModel>

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
                     }).Distinct();

            // ** LINQ always returns a sequence, so you have to retrieve the item out of it. **
            // There are four LINQ methods to retrieve a single item out of a sequence:
            // Single() returns the item, throws an exception if there are 0 or more than one item in the sequence.
            // SingleOrDefault() returns the item, or default value (null for string). Throws if more than one item in the sequence.
            // First() returns the first item. Throws if there are 0 items in the sequence.
            // FirstOrDefault() returns the first item, or the default value if there are no items).
            return View(q.Single());
        }

        // GET: Artworks/Owner
        public ActionResult Inventory()
        {
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
                         Cost = p.Cost,
                         Price = p.Price,
                         NumberMade = aws.NumberMade,
                         NumberSold = aws.NumberSold,
                     }).Distinct();

                //q = q.Where(s => s.NumberInInventory > 0);
                return View(q);
        }

        public ActionResult Sales()
        {
            var q = (from pcs in db.Pieces
                     join aw in db.Artworkz on pcs.ArtworkId equals aw.ArtworkId
                     join ar in db.Artists on aw.ArtistId equals ar.ArtistId
                     where pcs.SoldFor != null
                     select new
                     {
                         PieceId = pcs.PieceId,
                         ArtworkId = pcs.ArtworkId,
                         Cost = pcs.Cost,
                         Price = pcs.Price,
                         SoldFor = pcs.SoldFor,
                         EditionNumber = pcs.EditionNumber,
                         Title = aw.Title,
                         ArtistName = ar.Name
                     });

           // q is assigned type IQueryable< 'a> (anonymous)
            var qq = q.AsEnumerable().Select(xx => new PieceViewModel  // use this syntax when retrieving > 1 item
            {
                PieceId = xx.PieceId,
                ArtworkId = xx.ArtworkId,
                Cost = xx.Cost,
                Price = xx.Price,
                SoldFor = xx.SoldFor,
                EditionNumber = xx.EditionNumber,
                Title = xx.Title,
                ArtistName = xx.ArtistName
            }).ToList();

            for (int i = 0; i < qq.Count(); i++)
            {
                qq[i].Profit = Convert.ToDecimal(qq[i].SoldFor) - Convert.ToDecimal(qq[i].Cost);
            }

            return View(qq);
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
        public ActionResult Create([Bind(Include = "ArtworkId,ArtistId,Title,YearOriginalCreated,Medium,Category,Dimensions,NumberMade,NumberInInventory,NumberSold,EditionsAcquired,ImageURL,Location,Cost,Price")] ArtworkArtistPieceViewModel aapvm)
        {

            Artwork newArtwork = new Artwork
            {
                ArtworkId = aapvm.ArtworkId,
                ArtistId = aapvm.ArtistId,
                Title = aapvm.Title,
                YearOriginalCreated = aapvm.YearOriginalCreated,
                Medium = aapvm.Medium,
                Dimensions = aapvm.Dimensions,
                NumberMade = aapvm.NumberMade,
                NumberInInventory = aapvm.NumberInInventory,
                NumberSold = aapvm.NumberSold,
                Category = aapvm.Category
            };

            if (ModelState.IsValid)
            {
                db.Artworkz.Add(newArtwork);
                db.SaveChanges();
            }

            // parse EditionsAcquired string and create one new row in Piece table for each edition acquired
            // start by creating list of Edition Numbers
            string[] ea = aapvm.EditionsAcquired.Split(',');
            List<int> editions = new List<int>();  // initialize blank list to contain editions

            foreach (string ee in ea)
            {
                Regex rgx = new Regex(@"\x22");
                string e = rgx.Replace(ee, "");

                // does e represent a range?
                if (e.Contains('-'))
                {
                    string[] range = e.Split('-');
                    for (int m = Convert.ToInt16(range[0]); m <= Convert.ToInt16(range[1]); m++)
                    {
                        editions.Add(m);
                    }
                }
                else // e is individual edition, not a range
                {
                    editions.Add(Convert.ToInt16(e));
                }
            }

            // create new row in Piece for each edition
            int editionsLogged = 0;
            foreach (int ed in editions)
            {
                Piece newPiece = new Piece
                {
                    ArtworkId = db.Artworkz.Max(a => a.ArtworkId),
                    DateCreated = aapvm.YearOriginalCreated.ToString(),
                    Cost = aapvm.Cost,
                    Price = aapvm.Price,
                    Location = aapvm.Location,
                    ImageURL = aapvm.ImageURL
                };

                newPiece.EditionNumber = Convert.ToInt16(ed);

                newPiece.SoldFor = ++editionsLogged <= aapvm.NumberSold ?
                                       newPiece.Price : null;

                db.Pieces.Add(newPiece);
                db.Entry(newPiece).State = EntityState.Added; // do we need this? why?
                db.SaveChanges();
            }

            return RedirectToAction("Inventory");
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
                return RedirectToAction("Inventory");
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
            return RedirectToAction("Inventory");
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
