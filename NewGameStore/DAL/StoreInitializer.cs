using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using GameStore.Models;

namespace GameStore.DAL
{
    public class StoreInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<StoreContext>
    {
        protected override void Seed (StoreContext context)
        {
            var clients = new List<Client>
            {
                new Client {FirstMidName = "Adam", LastName = "Jensen", BirthDate = DateTime.Parse("01-01-1993")},
                new Client {FirstMidName = "Frank", LastName = "Pritchard", BirthDate = DateTime.Parse("01-01-1993")}
            };
            clients.ForEach(c => context.Clients.Add(c));
            context.SaveChanges();

            var genres = new List<Genre>
            {
                new Genre {Name = "FPS"},
                new Genre {Name = "Fighting"},
                new Genre {Name = "Racing"}
            };
            genres.ForEach(g => context.Genres.Add(g));
            context.SaveChanges();

            var publishers = new List<Publisher>
            {
                new Publisher {Name = "Namco" },
                new Publisher {Name = "Sega" },
                new Publisher {Name = "Nintendo" },
            };
            publishers.ForEach(p => context.Publishers.Add(p));
            context.SaveChanges();

            var games = new List<Game>
            {
                new Game {Title = "TEKKEN 7", ESRBID = 5, Description = "GotY 2017", Year = DateTime.Parse("03-01-2017"), Value = 59, PublisherID=1}
            };
            games.ForEach(g => context.Games.Add(g));
            context.SaveChanges();
        }
    }
}