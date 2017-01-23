using System;
using System.Collections.Generic;
using NewGameStore.Models;

namespace NewGameStore.DAL.Repositories
{
    public interface IClientRepository : IDisposable
    {
        IEnumerable<Client> GetClients();
        IEnumerable<Client> GetActiveClients();
        IEnumerable<Client> GetInactiveClients();
        IEnumerable<Game> AvailableGames(int age);
        Client GetClientByID(int? ClientID);
        void InsertClient(Client Client);
        void DeleteClient(int ClientID);
        void ActivateClient(int ClientID);
        void UpdateClient(Client Client);
        void Save();
    }
}