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
using System.Globalization;
using NewGameStore.ViewModels;
using NewGameStore.DAL.Repositories;
using System.Data.SqlClient;

namespace NewGameStore.Controllers
{
    public class GamesController : Controller
    {
        private IGameRepository gameRepository;

        public GamesController()
        {
            this.gameRepository = new GameRepository(new StoreContext());
        }

        public GamesController(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }
        // GET: Games
        public ActionResult Index(string searchString, int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 0);
            var games = gameRepository.GetGames();
            int maxPage = games.Count()/pageSize;

            if (page <= 0) pageNumber = 0;
            if (page >= maxPage) pageNumber = maxPage;
            

            ViewBag.CurrentPage = pageNumber;
            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper()));
                
            }
            games = games.OrderBy(g => g.Title);
            
            
            return View(games.Skip(pageSize * (pageNumber)).Take(pageSize).ToList());
        }
        
        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = gameRepository.GetGameByID(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.ESRBID = new SelectList(gameRepository.GetRatings(), "ESRBID", "Rating"); 
            ViewBag.PublisherID = new SelectList(gameRepository.getPublishers(), "PublisherID", "Name");
            ViewBag.GenreID = new SelectList(gameRepository.getGenres(), "GenreID", "Name");

            return View();
        }
        
        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameID,Title,ESRBID,Year,Description,Value,PublisherID, GenreID")] Game game)
        {
            ViewBag.ESRBID = new SelectList(gameRepository.GetRatings(), "ESRBID", "Rating");
            ViewBag.PublisherID = new SelectList(gameRepository.getPublishers(), "PublisherID", "Name");
            ViewBag.GenreID = new SelectList(gameRepository.getGenres(), "GenreID", "Name");
            if (ModelState.IsValid)
            {    
                gameRepository.InsertGame(game);
                gameRepository.Save();
                
            }
            return RedirectToAction("Index");
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = gameRepository.GetGameByID(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.ESRBID = new SelectList(gameRepository.GetRatings(), "ESRBID", "Rating");
            ViewBag.PublisherID = new SelectList(gameRepository.getPublishers(), "PublisherID", "Name");
            ViewBag.GenreID = new SelectList(gameRepository.getGenres(), "GenreID", "Name");
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameID,Title,ESRBID,Year,Description,Value,PublisherID, GenreID")] Game game)
        {
            if (ModelState.IsValid)
            {
                gameRepository.UpdateGame(game);
                gameRepository.Save();
                return RedirectToAction("Index");
            }
            //ViewBag.ESRBID = new SelectList(gameRepository.GetRatings(), "ESRBID", "Rating");
            //ViewBag.PublisherID = new SelectList(gameRepository.getPublishers(), "PublisherID", "Name");
            //ViewBag.GenreID = new SelectList(gameRepository.getGenres(), "GenreID", "Name");

            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = gameRepository.GetGameByID(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gameRepository.DeleteGame(id);
            gameRepository.Save();

            return RedirectToAction("Index");
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                gameRepository.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}
