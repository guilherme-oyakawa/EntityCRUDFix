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
    public class PublishersController : Controller
    {
        private IPublisherRepository pubRepository;

        public PublishersController()
        {
            this.pubRepository =  new PublisherRepository(new StoreContext());
        }


        // GET: Publishers
        public ActionResult Index()
        {
            return View(pubRepository.GetPublishers());
        }

        public ActionResult Games(int id)
        {
            var name = from p in pubRepository.GetPublishers()
                       where p.PublisherID == id
                       select p.Name;
            ViewBag.Publisher = (String)name.First();
            return View(pubRepository.GamesPerPublisher(id).OrderBy(g => g.Title));
        }

        // GET: Publishers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PublisherID,Name")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                pubRepository.InsertPublisher(publisher);
                pubRepository.Save();
                return RedirectToAction("Index");

            }

            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = pubRepository.GetPubByID(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = pubRepository.GetPubByID(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pubRepository.DeletePublisher(id);
            pubRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                pubRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
