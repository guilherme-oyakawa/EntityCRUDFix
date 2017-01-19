using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewGameStore.Models
{
    public class ESRB
    {
        public int ESRBID { get; set; }

        public string Rating { get; set; }

        public string Description { get; set; }

        public int Age { get; set; }
    }
}