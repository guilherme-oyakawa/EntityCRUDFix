using NewGameStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NewGameStore.DAL.Repositories
{
    public class CopyRepository : ICopyRepository, IDisposable
    {
        private StoreContext context;

        public CopyRepository(StoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Copy> GetCopies()
        {
            return context.Copies.ToList();
        }

        public Copy GetCopyByID(int? CopyID)
        {
            return context.Copies.Find(CopyID);
        }

        public void InsertCopy(Copy Copy)
        {
            context.Copies.Add(Copy);
        }

        public void DeleteCopy(int CopyID)
        {
            Copy Copy = context.Copies.Find(CopyID);
            context.Copies.Remove(Copy);
        }

        public void UpdateCopy(Copy Copy)
        {
            context.Entry(Copy).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public IEnumerable<Game> GetGames()
        {
            return context.Games.ToList();
        }

        public IEnumerable<Game> GetCopyGame(int? id)
        {
            var game = from c in context.Copies
                       where c.CopyID == id
                       select c.Game;
            return game.ToList();
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