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
    public class FeesController : Controller
    {
        private IFeeRepository feeRepository;
        public FeesController()
        {
            this.feeRepository = new FeeRepository(new StoreContext());
        }
        // GET: Fees
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            int pageSize = 4;
            int pageNumber = (page ?? 0);

            var fees = feeRepository.GetCurrentFees();
            int maxPage = fees.Count() / pageSize;

            switch (sortOrder)
            {
                case "Price":
                    fees = fees.OrderBy(f => f.Value);
                    break;
                case "price_desc":
                    fees = fees.OrderByDescending(f => f.Value);
                    break;
                default:
                    fees = fees.OrderBy(f => f.FeeID);
                    break;
            }

            if (page <= 0) pageNumber = 0;
            if (page >= maxPage) pageNumber = maxPage;
            ViewBag.CurrentPage = pageNumber;
            return View(fees.Skip(pageSize * (pageNumber)).Take(pageSize).ToList());
        }

        public ActionResult Log(string sortOrder, int? page)
        {
            ViewBag.NameSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            int pageSize = 4;
            int pageNumber = (page ?? 0);

            var fees = feeRepository.GetPaidFees();
            int maxPage = fees.Count() / pageSize;

            switch (sortOrder)
            {
                case "Price":
                    fees = fees.OrderBy(f => f.Value);
                    break;
                case "price_desc":
                    fees = fees.OrderByDescending(f => f.Value);
                    break;
                default:
                    fees = fees.OrderBy(f => f.FeeID);
                    break;
            }

            if (page <= 0) pageNumber = 0;
            if (page >= maxPage) pageNumber = maxPage;
            ViewBag.CurrentPage = pageNumber;
            return View(fees.Skip(pageSize * (pageNumber)).Take(pageSize).ToList());
        }

        // GET: Fees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = feeRepository.GetFeeByID(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // GET: Fees/Create
        public ActionResult Create()
        {
            ViewBag.RentalID = new SelectList(feeRepository.GetRentals(), "RentalID", "RentalID");
            return View();
        }

        // POST: Fees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FeeID,RentalID,Value,Paid")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                feeRepository.InsertFee(fee);
                feeRepository.Save();
                return RedirectToAction("Index");
            }

            return View(fee);
        }

        // GET: Fees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = feeRepository.GetFeeByID(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            ViewBag.RentalID = new SelectList(feeRepository.GetRentals(), "RentalID", "RentalID", fee.RentalID);
            return View(fee);
        }

        // GET: Rentals/Pay/5
        public ActionResult Pay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = feeRepository.GetFeeByID(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // POST: Rentals/Pay/5
        [HttpPost, ActionName("Pay")]
        [ValidateAntiForgeryToken]
        public ActionResult ReturnConfirmed(int id)
        {
            feeRepository.PayFee(id);
            //Fee fee = feeRepository.GetFeeByID(id);
            //fee.Paid = true;
            //feeRepository.UpdateFee(fee);
            //feeRepository.Save();
            return RedirectToAction("Index");
        }

        // POST: Fees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeeID,RentalID,Value,Paid")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                feeRepository.UpdateFee(fee);
                feeRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.RentalID = new SelectList(feeRepository.GetRentals(), "RentalID", "RentalID", fee.RentalID);
            return View(fee);
        }

        // GET: Fees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = feeRepository.GetFeeByID(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // POST: Fees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            feeRepository.DeleteFee(id);
            feeRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                feeRepository.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}
