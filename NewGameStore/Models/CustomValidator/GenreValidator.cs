using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using NewGameStore.Models;
using NewGameStore.DAL;
using NewGameStore.DAL.Repositories;

namespace NewGameStore.Models.CustomValidator
{
    public class GenreValidator : ValidationAttribute
    {
        private IGenreRepository genreRepository;

        public GenreValidator()
        {
            this.genreRepository = new GenreRepository(new StoreContext());
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                var names = from c in genreRepository.GetGenres()
                            select c.Name;
                string name = value.ToString();

                if (!names.ToList().Contains(name))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Genre already exists in database.");
            }
            return new ValidationResult("Please enter a valid name for the genre.");
        }
    }
}