using System.ComponentModel.DataAnnotations;

namespace CundecinosWeb.Enum
{
    public enum Calification
    {
        [Display(Name = "Muy Malo")]
        VeryBad = 10,

        [Display(Name = "Malo")]
        Bad = 20,


        [Display(Name = "Medio")]
        Medium = 30,


        [Display(Name = "Bueno")]
        Good = 40,

        [Display(Name = "Muy Bueno")]
        VeryGood = 50,

    }
}
