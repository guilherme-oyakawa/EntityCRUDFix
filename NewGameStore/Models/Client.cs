using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace NewGameStore.Models
{
    public class Client
    {
        public Client()
        {
            this.Active = true;
        }

        [Key]
        public int ClientID { get; set; }

        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [StringLength(50)]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [DefaultValue(true)]
        public bool Active { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstMidName + " " + LastName;
            }
        }

        public int Age
        {
            get
            {
                DateTime today = DateTime.Now;
                int age = (today - this.BirthDate).Days / 365;
                return age;
            }
        }

        public virtual ICollection<Rental> Rentals{ get; set; }

        public virtual ICollection<Fee> Fees { get; set; }
    }
}