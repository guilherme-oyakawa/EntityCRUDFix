using System;
using System.Collections.Generic;
using System.Linq;
using NewGameStore.Models;
using System.Data.Entity;
using System.Data.SqlClient;

namespace NewGameStore.DAL.Repositories
{
    public class ClientRepository : IClientRepository, IDisposable
    {
        private StoreContext context;

        public ClientRepository(StoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Client> GetClients()
        {
            return context.Clients.ToList();
        }

        public IEnumerable<Client> GetActiveClients()
        {
            var query = from c in context.Clients
                        where c.Active == true
                        select c;
            return (query.ToList());
        }

        public IEnumerable<Client> GetInactiveClients()
        {
            var query = from c in context.Clients
                        where c.Active == false
                        select c;
            return (query.ToList());
        }

    public Client GetClientByID(int? ClientID)
        {
            return context.Clients.Find(ClientID);
        }

        public void InsertClient(Client Client)
        {
            context.Clients.Add(Client);
        }

        public void DeleteClient(int ClientID)
        {

            context.Database.ExecuteSqlCommand("EXEC DeactivateClient @Client = {0}", ClientID);
            //Client Client = context.Clients.Find(ClientID);
            //context.Clients.Remove(Client);
        }

        public void ActivateClient(int ClientID)
        {

            context.Database.ExecuteSqlCommand("EXEC ActivateClient @Client = {0}", ClientID);
            //Client Client = context.Clients.Find(ClientID);
            //context.Clients.Remove(Client);
        }


        public void UpdateClient(Client Client)
        {
            context.Entry(Client).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}