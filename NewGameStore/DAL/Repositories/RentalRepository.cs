using NewGameStore.Models;
using NewGameStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NewGameStore.DAL.Repositories
{
    public class RentalRepository : IRentalRepository, IDisposable
    {
        private StoreContext context;
        public RentalRepository(StoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Rental> GetRentals()
        {
            return context.Rentals.ToList();
        }

        public IEnumerable<Rental> GetReturnedRentals()
        {
            var rentals = from rental in context.Rentals
                          where rental.ReturnedOn != null
                          select rental;
            return (rentals.ToList());
        }
        public IEnumerable<Rental> GetCurrentRentals()
        {
            var rentals = from rental in context.Rentals
                          where rental.ReturnedOn == null
                          select rental;
            return (rentals.ToList());
        }

        public Rental GetRentalByID(int? RentalID)
        {
            return context.Rentals.Find(RentalID);
        }

        public void InsertRental(Rental Rental)
        {
            context.Rentals.Add(Rental);
        }

        public void DeleteRental(int RentalID)
        {
            Rental Rental = context.Rentals.Find(RentalID);
            context.Rentals.Remove(Rental);
        }

        public void UpdateRental(Rental Rental)
        {
            context.Entry(Rental).State = EntityState.Modified;
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

        public IEnumerable<Copy> GetAvailableCopies()
        {
            var query = from copy in context.Copies
                        where copy.Available == true
                        orderby copy.Game.Title
                        select copy;
            return query.ToList();
        }

        public IEnumerable<Client> GetClients()
        {
            return context.Clients.ToList();
        }

        public IEnumerable<Copy> GetCopies()
        {
            return context.Copies.ToList();
        }

        public void InsertFee(Fee Fee)
        {
            context.Fees.Add(Fee);
        }

        public IEnumerable<TopClients> TopClients()
        {
            throw new NotImplementedException();
        }
    }
}