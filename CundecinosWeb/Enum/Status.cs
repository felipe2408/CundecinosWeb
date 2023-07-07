using System.ComponentModel.DataAnnotations;

namespace CundecinosWeb.Enum
{
    public enum Status
    {

        [Display(Name = "En Oferta")]
        Inoffer = 10,

        [Display(Name = "Negociación")]
        Negotiation = 20,

        [Display(Name = "Intercambiado")]
        Exchanged = 30,


    }
}
