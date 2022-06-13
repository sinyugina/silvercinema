using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Дипломный_проект.Models
{
    internal class TimeViewModel
    {
        [Display(Name = "Id")]
        public Int32 Id { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "Время")]
        public TimeSpan Time { get; set; }
    }
}
