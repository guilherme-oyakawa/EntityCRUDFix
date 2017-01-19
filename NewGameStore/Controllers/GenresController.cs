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
    public class GenresController : Controller
    {
        private IGenreRepository genreRepository;

        public GenresController()
        {
            this.genreRepository = new GenreRepository(new StoreContext());
        }

        // GET: Genres
        public ActionResult Index()
        {
            return View(genreRepository.GetGenres());
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenreID,Name")] Genre genre)
        {
            if (ModelState.IsValid)
            {

                genreRepository.InsertGenre(genre);
                genreRepository.Save();
                return RedirectToAction("Index");
            }

            return View(genre);
        }

       
        // GET: Genres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = genreRepository.GetGenreByID(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            genreRepository.DeleteGenre(id);
            genreRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                genreRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
