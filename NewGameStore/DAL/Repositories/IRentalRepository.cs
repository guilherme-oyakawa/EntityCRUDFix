using NewGameStore.Models;
using NewGameStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGameStore.DAL.Repositories
{
    interface IRentalRepository : IDisposable
    {
        IEnumerable<Rental> GetRentals();
        IEnumerable<Rental> GetReturnedRentals();
        IEnumerable<Rental> GetCurrentRentals();

        IEnumerable<Client> GetClientsWithRentals();

        IEnumerable<Rental> GetRentalsPerClient(int? id);

        Rental GetRentalByID(int? RentalID);
        void InsertRental(Rental Rental);
        void DeleteRental(int RentalID);
        void UpdateRental(Rental Rental);
        IEnumerable<Copy> GetAvailableCopies();
        //IEnumerable<Client> GetClients();
        IEnumerable<Client> GetActiveClients();
        
        IEnumerable<Copy> GetCopies();
        IEnumerable<TopClients> TopClients();
        void InsertFee(Fee Fee);
        void ReturnCopy(int CopyID);
        void Save();
    }
}
