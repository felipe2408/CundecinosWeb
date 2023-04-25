using System.ComponentModel.DataAnnotations;

namespace CundecinosWeb.Enum
{
    public enum PublicationType
    {
        [Display(Name = "En Venta")]
        Sale = 10,

        [Display(Name = "Trueque")]
        Barter = 20,
    }
}
