using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewGameStore.DAL;
using NewGameStore.ViewModels;
using NewGameStore.DAL.Repositories;

namespace NewGameStore.Controllers
{
    public class HomeController : Controller
    {
        private IRentalRepository rentalRepository;

        public HomeController()
        {
            this.rentalRepository = new RentalRepository(new StoreContext());
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IEnumerable<TopRents> data = from rental in rentalRepository.GetRentals()
                                        group rental by (rental.Copy.Details) into CopyGroup
                                        select new TopRents()
                                        {
                                            GameDetails = CopyGroup.Key,
                                            RentCount = CopyGroup.Count()
                                        };
            return View(data.OrderByDescending(tr=> tr.RentCount).ToList());
        }

        public ActionResult TopCustomers()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            rentalRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}