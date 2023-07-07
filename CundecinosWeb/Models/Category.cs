using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CundecinosWeb.Models
{
	public class Category
	{
        public Guid CategoryID { get; set; }

		[Display(Name = "Nombre")]
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[Column(TypeName = "varchar(100)")]
		public String? Name { get; set; }


		[Display(Name = "Registro vigente?")]
		[Required(ErrorMessage = "El campo {0} es requerido")]
		public bool IsActive { get; set; }

		[JsonIgnore]
		public virtual ICollection<Publication>? Publication { get; set; }
	}
}
