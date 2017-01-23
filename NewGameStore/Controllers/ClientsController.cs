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
    public class ClientsController : Controller
    {
        private IClientRepository clientRepository;

        public ClientsController()
        {
            this.clientRepository = new ClientRepository(new StoreContext());
        }

        public ClientsController(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }
            
        // GET: Clients
        public ActionResult Index(string searchString, int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 0);
            var clients = clientRepository.GetActiveClients();
            int maxPage = clients.Count() / pageSize;

            if (page <= 0) pageNumber = 0;
            if (page >= maxPage) pageNumber = maxPage;

            ViewBag.CurrentPage = pageNumber;
            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(c => c.FullName.ToUpper().Contains(searchString.ToUpper()));
            }
            clients = clients.OrderBy(c => c.LastName);
            return View(clients.Skip(pageSize * (pageNumber)).Take(pageSize).OrderBy(c=> c.FirstMidName).ToList());
        }

        public ActionResult GamesPerAge(int id)
        {
            Client client = clientRepository.GetClientByID(id);
            ViewBag.Name = client.FullName;
            ViewBag.Age = client.Age;
            IEnumerable<Game> list = clientRepository.AvailableGames(client.Age);
            return View(list.OrderBy(g => g.Title).OrderBy(g=> g.ESRBID));
        }

        public ActionResult Log(string searchString, int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 0);
            var clients = clientRepository.GetInactiveClients();
            int maxPage = clients.Count() / pageSize;

            if (page <= 0) pageNumber = 0;
            if (page >= maxPage) pageNumber = maxPage;

            ViewBag.CurrentPage = pageNumber;
            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(c => c.FullName.ToUpper().Contains(searchString.ToUpper()));
            }
            clients = clients.OrderBy(c => c.LastName);
            return View(clients.Skip(pageSize * (pageNumber)).Take(pageSize).ToList());
        }


        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = clientRepository.GetClientByID(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientID,FirstMidName,LastName,BirthDate")] Client client)
        {
            if (ModelState.IsValid)
            {
                clientRepository.InsertClient(client);
                clientRepository.Save();
                return RedirectToAction("Index");
            }

            return View(client);
        }
        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = clientRepository.GetClientByID(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientID,FirstMidName,LastName,BirthDate")] Client client)
        {
            if (ModelState.IsValid)
            {
                clientRepository.UpdateClient(client);
                clientRepository.Save();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = clientRepository.GetClientByID(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            clientRepository.DeleteClient(id);
            clientRepository.Save();
            return RedirectToAction("Index");
        }

        // GET: Clients/Activate/5
        public ActionResult Activate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = clientRepository.GetClientByID(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Activate/5
        [HttpPost, ActionName("Activate")]
        [ValidateAntiForgeryToken]
        public ActionResult ActivateConfirmed(int id)
        {
            clientRepository.ActivateClient(id);
            clientRepository.Save();
            return RedirectToAction("Log");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                clientRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
