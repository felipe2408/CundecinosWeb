using System.ComponentModel.DataAnnotations;

namespace CundecinosWeb.Enum
{
    public enum PublicationType
    {
        [Display(Name = "Vender")]
        Sale = 10,

        [Display(Name = "Trueque")]
        Barter = 20,
    }
}
