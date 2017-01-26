using NewGameStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGameStore.DAL.Repositories
{
    interface ICopyRepository: IDisposable
    {
        IEnumerable<Copy> GetCopies();
        Copy GetCopyByID(int? CopyID);
        void InsertCopy(Copy Copy);
        void DeleteCopy(int CopyID);
        void UpdateCopy(Copy Copy);
        IEnumerable<Game> GetGames();

        IEnumerable<Game> GetCopyGame(int? id);
        void Save();
    }
}
