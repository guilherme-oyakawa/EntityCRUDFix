using NewGameStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NewGameStore.DAL.Repositories
{
    public interface IFeeRepository : IDisposable
    {
        IEnumerable<Fee> GetFees();
        IEnumerable<Fee> GetCurrentFees();
        IEnumerable<Fee> GetPaidFees();

        IEnumerable<Fee> GetFeesPerClient(int? id);

        IEnumerable<Client> GetClientsWithFees();

        Fee GetFeeByID(int? FeeID);
        void InsertFee(Fee Fee);
        void DeleteFee(int FeeID);
        void UpdateFee(Fee Fee);

        void PayFee(int FeeID);

        IEnumerable<Rental> GetRentals();
        void Save();
    }
}