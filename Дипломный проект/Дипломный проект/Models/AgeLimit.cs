using System.ComponentModel.DataAnnotations;

namespace Дипломный_проект.Models
{
    public enum AgeLimit
    {
        [Display(Name = "0+")]
        Zero = 1,

        [Display(Name = "6+")]
        Six = 2,

        [Display(Name = "12+")]
        Twelve = 3,

        [Display(Name = "16+")]
        Sixteen = 4,

        [Display(Name = "18+")]
        Eighteen = 5
    }
}
