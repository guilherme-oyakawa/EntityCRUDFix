using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewGameStore.DAL;
using NewGameStore.Models;
using NewGameStore.DAL.Repositories;

namespace NewGameStore.Controllers
{
    public class RentalsController : Controller
    {
        private IRentalRepository rentalRepository;
        public RentalsController()
        {
            this.rentalRepository = new RentalRepository(new StoreContext());
        }
       
        // GET: Rentals
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            int pageSize = 4;
            int pageNumber = (page ?? 0);
            var rentals = rentalRepository.GetCurrentRentals();
            int maxPage = rentals.Count() / pageSize;

            switch (sortOrder)
            {
                case "Price":
                    rentals = rentals.OrderBy(r => r.Price);
                    break;
                case "price_desc":
                    rentals = rentals.OrderByDescending(r => r.Price);
                    break;
                case "Date":
                    rentals = rentals.OrderBy(r => r.DueDate);
                    break;
                case "date_desc":
                    rentals = rentals.OrderByDescending(r => r.DueDate);
                    break;
                default:
                    rentals = rentals.OrderBy(r => r.DueDate);
                    break;
            }

            if (page <= 0) pageNumber = 0;
            if (page >= maxPage) pageNumber = maxPage;


            ViewBag.CurrentPage = pageNumber;
            return View(rentals.Skip(pageSize * (pageNumber)).Take(pageSize).ToList());
        }

        // GET: Rentals/Log
        public ActionResult Log(string sortOrder, int? page)
        {
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            int pageSize = 4;
            int pageNumber = (page ?? 0);

            var rentals = rentalRepository.GetReturnedRentals();
            int maxPage = rentals.Count() / pageSize;

            switch (sortOrder)
            {
                case "Price":
                    rentals = rentals.OrderBy(r => r.Price);
                    break;
                case "price_desc":
                    rentals = rentals.OrderByDescending(r => r.Price);
                    break;
                case "Date":
                    rentals = rentals.OrderBy(r => r.ReturnedOn);
                    break;
                case "date_desc":
                    rentals = rentals.OrderByDescending(r => r.ReturnedOn);
                    break;
                default:
                    rentals = rentals.OrderBy(r => r.DueDate);
                    break;
            }

            if (page <= 0) pageNumber = 0;
            if (page >= maxPage) pageNumber = maxPage;


            ViewBag.CurrentPage = pageNumber;
            return View(rentals.Skip(pageSize * (pageNumber)).Take(pageSize).ToList());
        }

        // GET: Rentals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = rentalRepository.GetRentalByID(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // GET: Rentals/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(rentalRepository.GetClients(), "ClientID", "FullName");
            ViewBag.CopyID = new SelectList(rentalRepository.GetAvailableCopies(), "CopyID", "Details");
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentalID,LentOn,DueDate,Price,ReturnedOn,ClientID,CopyID")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                var query = from copy in rentalRepository.GetCopies()
                            where copy.CopyID == rental.CopyID
                            select copy;

                foreach(Copy copy in query)
                {
                    copy.Available = false;
                }

                rentalRepository.InsertRental(rental);
                rentalRepository.Save();
                return RedirectToAction("Index");
            }
            return View(rental);
        }

        // GET: Rentals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = rentalRepository.GetRentalByID(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(rentalRepository.GetClients(), "ClientID", "FullName", rental.ClientID);
            ViewBag.CopyID = new SelectList(rentalRepository.GetCopies(), "CopyID", "Details", rental.CopyID);
            return View(rental);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalID,LentOn,DueDate,Price,ReturnedOn,ClientID,CopyID")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                rentalRepository.UpdateRental(rental);
                rentalRepository.Save();

                return RedirectToAction("Index");
            }
            return View(rental);
        }

        // GET: Rentals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = rentalRepository.GetRentalByID(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var query = from rental in rentalRepository.GetCurrentRentals()
                        where rental.RentalID == id
                        select rental;

            foreach(Rental rental in query)
            {
                rental.Copy.Available = true;
            }

            rentalRepository.DeleteRental(id);
            rentalRepository.Save();
            return RedirectToAction("Index");
        }

        // GET: Rentals/Return/5
        public ActionResult Return(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = rentalRepository.GetRentalByID(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // POST: Rentals/Return/5
        [HttpPost, ActionName("Return")]
        [ValidateAntiForgeryToken]
        public ActionResult ReturnConfirmed(int id)
        {
            Rental rental = rentalRepository.GetRentalByID(id);
            DateTime ret = DateTime.Now;
            rental.Copy.Available = true;
            rental.ReturnedOn = ret;
            Fee fee = new Fee
            {
                RentalID = rental.RentalID,
                Rental = rental,
                Value = rental.RentalFee,
                Paid = false
            };
            if(fee.Value > 0)
            {
                rentalRepository.InsertFee(fee);
                rentalRepository.Save();
            }
            rentalRepository.UpdateRental(rental);
            rentalRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                rentalRepository.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}
