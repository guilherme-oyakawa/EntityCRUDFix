using System;
using System.Collections.Generic;
using NewGameStore.Models;

namespace NewGameStore.DAL.Repositories
{
    public interface IClientRepository : IDisposable
    {
        IEnumerable<Client> GetClients();
        Client GetClientByID(int? ClientID);
        void InsertClient(Client Client);
        void DeleteClient(int ClientID);
        void UpdateClient(Client Client);
        void Save();
    }
}