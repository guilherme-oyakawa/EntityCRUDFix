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
        public int ID { get; set; }
        public string FullName { get; set; }
        public decimal total { get; set; }
    }
}