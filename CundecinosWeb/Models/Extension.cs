
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CundecinosWeb.Models
{
	public class Extension
	{
        [Key]
		public Guid ExtensionId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(100)")]
        public String? Name { get; set; }


        [Display(Name = "Registro vigente?")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public bool IsActive { get; set; }

        [JsonIgnore]
        public Person? Person { get; set; }

    }
}
