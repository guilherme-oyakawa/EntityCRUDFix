using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewGameStore.ViewModels
{
    public class TopClients
    {
        public int id;
        public string FullName;
        public decimal total;
    }
}