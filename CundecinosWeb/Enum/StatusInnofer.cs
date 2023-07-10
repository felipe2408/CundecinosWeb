using System.ComponentModel.DataAnnotations;

namespace CundecinosWeb.Enum
{
    public enum StatusInnofer
    {


        [Display(Name = "Creada")]
        Create = 0,


        [Display(Name = "En Negociación")]
        InNegotiation = 10,

        [Display(Name = "Concluida")]
        Completed = 20,

    }
}
