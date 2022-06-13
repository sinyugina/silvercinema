using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Дипломный_проект.Models
{
    internal class CityViewModel
    {
        [Display(Name = "Id")]
        [Browsable(false)]
        public Int32 Id { get; set; }

        [Display(Name = "Название")]
        public String Name { get; set; }
    }
}
