using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Дипломный_проект.Models
{
    internal class PlaceViewModel
    {
        [Display(Name = "Id")]
        [Browsable(false)]
        public Int32 Id { get; set; }

        [Display(Name = "Ряд")]
        public Int32 Row { get; set; }

        [Display(Name = "Место")]
        public Int32 Place { get; set; }
    }
}
