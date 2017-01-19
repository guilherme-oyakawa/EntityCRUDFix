using NewGameStore.Models.CustomValidator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewGameStore.Models
{
    public class Genre
    {
        public int GenreID{ get; set; }

        [StringLength(30)]
        [Index(IsUnique = true)]
        [Display(Name = "Genre")]
        [GenreValidator]
        public string Name { get; set; }
    }
}