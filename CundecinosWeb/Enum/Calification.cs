using System.ComponentModel.DataAnnotations;

namespace CundecinosWeb.Enum
{
    public enum Calification
    {
        [Display(Name = "Muy Malo")]
        VeryBad = 1,

        [Display(Name = "Malo")]
        Bad = 2,


        [Display(Name = "Medio")]
        Medium = 3,


        [Display(Name = "Bueno")]
        Good = 4,

        [Display(Name = "Muy Bueno")]
        VeryGood = 5,

    }
}
