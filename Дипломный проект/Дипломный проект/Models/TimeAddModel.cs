using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Дипломный_проект.Models
{
    internal class TimeAddModel
    {
        [Display(Name = "Id")]
        public Int32 Id { get; set; }

        [Display(Name = "Дата и время")]
        public String DateTime { get; set; }
    }
}
