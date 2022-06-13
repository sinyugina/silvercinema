using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Дипломный_проект.Models
{
    internal class FilmViewModel
    {
        [Display(Name = "Id")]
        [Browsable(false)]
        public Int32 Id { get; set; }

        [Display(Name = "Название")]
        public String Name { get; set; }

        [Display(Name = "Описание")]
        public String Description { get; set; }

        [Display(Name = "Цена")]
        public Int32 Price { get; set; }

        [Display(Name = "Возрастное_ограничение")]
        public String AgeLimit { get; set; }

        [Display(Name = "Длительность")]
        public String Duration { get; set; }

        [Display(Name = "Жанр")]
        public String Genre { get; set; }

        [Display(Name = "Постер")]
        public byte[] Poster { get; set; }
    }
}
