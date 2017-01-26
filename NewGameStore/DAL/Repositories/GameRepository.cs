using System;
using System.Collections.Generic;
using System.Linq;
using NewGameStore.Models;
using System.Data.Entity;

namespace NewGameStore.DAL.Repositories
{
    public class GameRepository : IGameRepository, IDisposable
    {
        private StoreContext context;
        public GameRepository(StoreContext context)
        {
            this.context = context;
        }

        public IEnumerable<Game> GetGames()
        {
            return context.Games.ToList();
        }

        public Game GetGameByID(int? GameID)
        {
            return context.Games.Find(GameID);
        }

        public void InsertGame(Game game)
        {
            context.Games.Add(game);
        }

        public void DeleteGame(int GameID)
        {
            Game game = context.Games.Find(GameID);
            context.Games.Remove(game);
        }

        public void UpdateGame(Game Game)
        {
            context.Entry(Game).State = EntityState.Modified;
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

        public IEnumerable<Genre> getGenres()
        {
            return context.Genres.ToList();
        }

        public IEnumerable<Publisher> getPublishers()
        {
            return context.Publishers.ToList();
        }

        public IEnumerable<ESRB> GetRatings()
        {
            return context.Ratings.ToList();
        }

        public bool gameExists(Game game)
        {
            var exists = from g in context.Games
                         where game.Title == g.Title
                         select g;

            if (exists != null) return true;
            else return false;
            
        }

        public IEnumerable<Game> GetGamesByRating(int age)
        {
            var games = from g in context.Games
                        where g.ESRB.Age <= age
                        select g;
            return (games.OrderBy(g => g.Title).ToList());
        }

    }
}