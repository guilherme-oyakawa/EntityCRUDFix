using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewGameStore.Models
{
    public class Rental
    {
        public int RentalID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name ="Lent on")]
        public DateTime LentOn { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Returned on")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReturnedOn { get; set; }

        [ForeignKey("Client")]
        public int ClientID { get; set; }

        [ForeignKey("Copy"), Required]
        public int CopyID { get; set; }

        [Display(Name = "Rental + Fees")]
        [DataType(DataType.Currency)]
        public decimal TotalValue
        {
            get
            {
                return this.Price + this.RentalFee;
            }
        }

        public virtual Copy Copy { get; set; }

        public virtual Client Client { get; set; }


        [Display(Name = "Delay Fees")]
        [DataType(DataType.Currency)]

        public decimal RentalFee
        {
            get
            {
                DateTime ret = DateTime.Now;
                decimal value = 0;
                if (DateTime.Compare(ret, this.DueDate) > 0)
                {
                    if (this.Copy == null) return 0;

                    int delay = (ret - this.DueDate).Days;
                    try
                    {
                        value = (Decimal)this.Copy.Game.Value * delay * 0.1m;
                    }
                    catch (NullReferenceException)
                    {
                        //NullReferenceException
                        ;
                    }
                }
                return value;
            }
        }

    }
}