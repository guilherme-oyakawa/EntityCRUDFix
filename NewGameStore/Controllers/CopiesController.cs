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
using NewGameStore.ViewModels;
using NewGameStore.DAL.Repositories;

namespace NewGameStore.Controllers
{
    public class CopiesController : Controller
    {
        private ICopyRepository copyRepository;
        public CopiesController()
        {
            this.copyRepository = new CopyRepository(new StoreContext());
        }

        // GET: Copies
        public ActionResult Index()
        {

            IEnumerable<CopiesPerGame> data = from copy in copyRepository.GetCopies()
                                              join juego in copyRepository.GetGames() on copy.GameID equals juego.GameID
                                              group copy by (copy.Details) into CopyGroup
                                              select new CopiesPerGame()
                                              {
                                                  GameTitle = CopyGroup.Key,
                                                  CopiesCount = CopyGroup.Count()
                                              };
            data = data.OrderBy(d => d.GameTitle);
            return View(data.ToList());
        }

        // GET: Copies/Details/5
        public ActionResult Details(string id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<Copy> data = from copy in copyRepository.GetCopies()
                                    where copy.Details == id
                                    select copy;
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data.ToList());
        }

        // GET: Copies/Create
        public ActionResult Create()
        {
            ViewBag.GameID = new SelectList(copyRepository.GetGames().OrderBy(g => g.Title), "GameID", "Details");
            return View();
        }

        // POST: Copies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameID,Available")] Copy copy, int? qty)
        {
            if (ModelState.IsValid)
            {
                if (qty == null) qty = 1;
                for (int i = 0; i < qty; i++)
                {
                    copyRepository.InsertCopy(copy);
                    copyRepository.Save();
                }
                return RedirectToAction("Index");
            }

            return View(copy);
        }

        // GET: Copies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Copy copy = copyRepository.GetCopyByID(id);
            if (copy == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameID = new SelectList(copyRepository.GetCopyGame(id), "GameID", "Details", copy.GameID);
            return View(copy);
        }

        // POST: Copies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CopyID,GameID,Available")] Copy copy)
        {
            if (ModelState.IsValid)
            {
                copyRepository.UpdateCopy(copy);
                copyRepository.Save();
                return RedirectToAction("Index");
            }
            return View(copy);
        }

        // GET: Copies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Copy copy = copyRepository.GetCopyByID(id);
            if (copy == null)
            {
                return HttpNotFound();
            }
            return View(copy);
        }

        // POST: Copies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            copyRepository.DeleteCopy(id);
            copyRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                copyRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
