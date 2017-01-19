using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewGameStore.Models;
using System.Data.Entity;

namespace NewGameStore.DAL.Repositories
{
    public class PublisherRepository : IPublisherRepository, IDisposable
    {
        private StoreContext context;

        public PublisherRepository(StoreContext context)
        {
            this.context = context;
        }

        public Publisher GetPubByID(int? id)
        {
            return context.Publishers.Find(id);
        }

        public IEnumerable<Publisher> GetPublishers()
        {
            return context.Publishers.ToList();
        }

        public Publisher GetPublisherByID(int? PublisherID)
        {
            return context.Publishers.Find(PublisherID);
        }

        public void InsertPublisher(Publisher Publisher)
        {
            context.Publishers.Add(Publisher);
        }

        public void DeletePublisher(int PublisherID)
        {
            Publisher Publisher = context.Publishers.Find(PublisherID);
            context.Publishers.Remove(Publisher);
        }

        public void UpdatePublisher(Publisher Publisher)
        {
            context.Entry(Publisher).State = EntityState.Modified;
        }

        public bool publisherExists(Publisher Publisher)
        {
            var exists = from p in context.Publishers
                         where Publisher.Name == p.Name
                         select p;

            return (exists != null) ? true : false;
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