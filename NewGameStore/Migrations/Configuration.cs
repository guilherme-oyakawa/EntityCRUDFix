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
            /*Only run this once
            
            var clients = new List<Client>
            {
                new Client {ClientID = 1, FirstMidName = "Adam", LastName = "Jensen", BirthDate = DateTime.Parse("01-01-1993")},
                new Client {ClientID = 2, FirstMidName = "Frank", LastName = "Pritchard", BirthDate = DateTime.Parse("01-01-1993")}
            };
            clients.ForEach(c => context.Clients.Add(c));
            context.SaveChanges();

            var ratings = new List<ESRB>
            {
                new ESRB {ESRBID = 1, Rating = "RP", Description = "Rating Pending", Age = 0},
                new ESRB {ESRBID = 2, Rating = "E", Description = "Everyone", Age = 3},
                new ESRB {ESRBID = 3, Rating = "E10+", Description = "Everyone 10+", Age = 10},
                new ESRB {ESRBID = 4 ,Rating = "T", Description = "Teen", Age = 13},
                new ESRB {ESRBID = 5, Rating = "M", Description = "Mature", Age = 17},
                new ESRB {ESRBID = 6, Rating = "AO", Description = "Adults Only", Age = 18}
            };
            ratings.ForEach(r => context.Ratings.AddOrUpdate(r));
            context.SaveChanges();

            var genres = new List<Genre>
            {
                new Genre {GenreID = 1, Name = "FPS" },
                new Genre {GenreID = 2, Name = "Fighting" },
                new Genre {GenreID = 3, Name = "Racing" }
            };
            genres.ForEach(g => context.Genres.AddOrUpdate(g));
            context.SaveChanges();

            var publishers = new List<Publisher>
            {
                new Publisher {PublisherID = 1, Name = "Nintendo" },
                new Publisher {PublisherID = 2, Name = "Sega" },
                new Publisher {PublisherID = 3, Name = "Electronic Arts" },
                new Publisher {PublisherID = 4, Name = "Namco" },
            };
            publishers.ForEach(p => context.Publishers.AddOrUpdate(p));
            context.SaveChanges();

            var games = new List<Game>
            {
                new Game {GameID = 1, Title="TEKKEN 7", ESRBID = 4, Description = "GotY 2017", Year = DateTime.Parse("03-01-2017"), Value = 59, PublisherID=4},
                new Game {GameID = 2, Title="Battlefield 1", ESRBID = 5, Description = "", Year = DateTime.Parse("03-01-2016"), Value = 59, PublisherID=3}
            };
            games.ForEach(g => context.Genres.AddOrUpdate(g));
            context.SaveChanges();

            var copies = new List<Copy>
            {
                new Copy {GameID = 1},
                new Copy {GameID = 2}
            };
            copies.ForEach(c => context.Copies.Add(c));
            context.SaveChanges();
            */
        }
    }
}
