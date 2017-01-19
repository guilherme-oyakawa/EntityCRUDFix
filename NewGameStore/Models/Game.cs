using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewGameStore.Models
{
    public class Game
    {
        public int GameID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Year { get; set; }

        public string Description { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Value { get; set; }
        
        [ForeignKey("Genre")]
        public int GenreID { get; set; }


        [ForeignKey("Publisher")]
        public int PublisherID { get; set; }

        [ForeignKey("ESRB")]
        public int ESRBID { get; set; }

        public string Details
        {
            get
            {
                return (this.Title + " (" + this.Publisher.Name + ", " + this.Year.Year + ")");

            }
        }

        public virtual ESRB ESRB { get; set; }
        public virtual Publisher Publisher { get; set; }

        public virtual Genre Genre { get; set; }

    }
}