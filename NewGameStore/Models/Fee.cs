using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewGameStore.Models
{
    public class Fee
    {
        [Key]
        public int FeeID { get; set; }

        [ForeignKey("Rental")]
        public int RentalID { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Value { get; set; }

        [DefaultValue(false)]
        public bool Paid { get; set; }

        public virtual Rental Rental { get; set; }
    }
}