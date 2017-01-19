using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewGameStore.ViewModels
{
    public class CopiesPerGame
    {
        public string GameTitle { get; set; }

        [Display (Name = "# of Copies")]
        public int CopiesCount{ get; set; }

    }
}