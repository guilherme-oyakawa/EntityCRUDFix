using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewGameStore.Models;
using System.Data.Entity;

namespace NewGameStore.DAL.Repositories
{
    public interface IGameRepository : IDisposable
    {
        IEnumerable<Game> GetGames();
        Game GetGameByID(int? GameID);
        void InsertGame(Game Game);
        void DeleteGame(int GameID);
        void UpdateGame(Game Game);
        void Save();
        bool gameExists(Game game);
        IEnumerable<Genre> getGenres();
        IEnumerable<Publisher> getPublishers();
        IEnumerable<ESRB> GetRatings();

        IEnumerable<Game> GetGamesByRating(int rating);
    }
}