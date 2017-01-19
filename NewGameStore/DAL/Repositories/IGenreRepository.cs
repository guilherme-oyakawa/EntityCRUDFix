﻿using NewGameStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGameStore.DAL.Repositories
{
    interface IGenreRepository : IDisposable
    {
        IEnumerable<Genre> GetGenres();

        Genre GetGenreByID(int? id);

        void InsertGenre(Genre Genre);

        void DeleteGenre(int GenreID);

        void UpdateGenre(Genre Genre);

        bool genreExists(Genre Genre);
        void Save();
    }
}