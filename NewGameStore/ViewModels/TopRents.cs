using NewGameStore.DAL;
using NewGameStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewGameStore.ViewModels
{
    public class TopRents
    {
        public string GameDetails{ get; set; }

        public int RentCount { get; set; }
    }
}