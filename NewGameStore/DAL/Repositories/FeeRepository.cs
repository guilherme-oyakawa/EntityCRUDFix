using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewGameStore.Models;
using System.Data.Entity;

namespace NewGameStore.DAL.Repositories
{
    public class FeeRepository : IFeeRepository, IDisposable
    {
        private StoreContext context;

        public FeeRepository(StoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Fee> GetFees()
        {
            return context.Fees.ToList();
        }

        public IEnumerable<Fee> GetCurrentFees()
        {
            var fees = from fee in context.Fees
                       where fee.Paid == false
                       select fee;
            return fees;
        }
        public IEnumerable<Fee> GetPaidFees()
        {
            var fees = from fee in context.Fees
                       where fee.Paid == true
                       select fee;
            return fees.ToList();

        }

        public Fee GetFeeByID(int? FeeID)
        {
            return context.Fees.Find(FeeID);
        }

        public void InsertFee(Fee Fee)
        {
            context.Fees.Add(Fee);
        }

        public void DeleteFee(int FeeID)
        {
            Fee Fee = context.Fees.Find(FeeID);
            context.Fees.Remove(Fee);
        }

        public void UpdateFee(Fee Fee)
        {
            context.Entry(Fee).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public IEnumerable<Rental> GetRentals()
        {
            return context.Rentals.ToList();
        }

        public void PayFee(int FeeID)
        {
            context.Database.ExecuteSqlCommand("EXEC PayFee @Fee = {0}", FeeID);
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