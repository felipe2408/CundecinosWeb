using System.ComponentModel.DataAnnotations;

namespace CundecinosWeb.Enum
{
    public enum PublicationType
    {
        [Display(Name = "Emprendimiento")]
        Sale = 10,

        [Display(Name = "Trueque")]
        Barter = 20,
    }
}
