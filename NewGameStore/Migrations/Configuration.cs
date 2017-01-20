using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using NewGameStore.Models;


namespace NewGameStore.Migrations
{


    internal sealed class Configuration : DbMigrationsConfiguration<NewGameStore.DAL.StoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NewGameStore.DAL.StoreContext context)
        {
            /*

            var clients = new List<Client>
            {
                new Client {FirstMidName = "Adam", LastName = "Jensen", BirthDate = DateTime.Parse("01-01-1993")},
                new Client {FirstMidName = "Frank", LastName = "Pritchard", BirthDate = DateTime.Parse("01-01-1993")}
            };
            clients.ForEach(c => context.Clients.Add(c));
            context.SaveChanges();

            var ratings = new List<ESRB>
            {
                new ESRB {Rating = "RP", Description = "Rating Pending", Age = 0},
                new ESRB {Rating = "E", Description = "Everyone", Age = 3},
                new ESRB {Rating = "E10+", Description = "Everyone 10+", Age = 10},
                new ESRB {Rating = "T", Description = "Teen", Age = 13},
                new ESRB {Rating = "M", Description = "Mature", Age = 17},
                new ESRB {Rating = "AO", Description = "Adults Only", Age = 18}
            };
            ratings.ForEach(r => context.Ratings.AddOrUpdate(r));
            context.SaveChanges();

            var genres = new List<Genre>
            {
                new Genre {Name = "FPS" },
                new Genre {Name = "Fighting" },
                new Genre {Name = "Racing" }
            };
            genres.ForEach(g => context.Genres.AddOrUpdate(g));
            context.SaveChanges();

            var publishers = new List<Publisher>
            {
                new Publisher {Name = "Nintendo" },
                new Publisher {Name = "Sega" },
                new Publisher {Name = "Electronic Arts" },
                new Publisher {Name = "Namco" },
            };
            publishers.ForEach(p => context.Publishers.AddOrUpdate(p));
            context.SaveChanges();

            var games = new List<Game>
            {
                new Game {Title="TEKKEN 7", ESRBID = 4, Description = "GotY 2017", Year = DateTime.Parse("03-01-2017"), Value = 59, PublisherID=4},
                new Game {Title="Battlefield 1", ESRBID = 5, Description = "", Year = DateTime.Parse("03-01-2016"), Value = 59, PublisherID=3}
            };
            games.ForEach(g => context.Genres.AddOrUpdate(g));
            context.SaveChanges();

            var copies = new List<Copy>
            {
                new Copy {GameID = 1},
                new Copy {GameID = 1},
                new Copy {GameID = 1},
                new Copy {GameID = 2},
                new Copy {GameID = 2},
                new Copy {GameID = 2},
                new Copy {GameID = 2},
                new Copy {GameID = 2}
            };
            copies.ForEach(c => context.Copies.Add(c));
            context.SaveChanges();
            */
        }
    }
}
