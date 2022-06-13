using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Дипломный_проект.Models
{
    public class SessionViewModel
    {
        [Display(Name = "Id")]
        [Browsable(false)]
        public Int32 Id { get; set; }

        [Display(Name = "Название фильма")]
        public String FilmName { get; set; }

        [Display(Name = "Город")]
        public String City { get; set; }

        [Display(Name = "Время")]
        public String Time { get; set; }

        [Display(Name = "Дата")]
        public String Date { get; set; }

        [Display(Name = "Цена")]
        public String Price { get; set; }

        [Display(Name = "Возрастное ограничение")]
        public String AgeLimit { get; set; }

        [Display(Name = "Описание")]
        public String Description { get; set; }

        [Display(Name = "Постер")]
        public byte[] Poster { get; set; }
    }
}
