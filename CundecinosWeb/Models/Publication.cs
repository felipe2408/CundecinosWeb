using CundecinosWeb.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CundecinosWeb.Models
{
    public class Publication
    {
        [Key]
        public Guid PublicationID { get; set; }

        public Guid PersonID { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(100)")]
        public string? Qualification { get; set; }

        [Display(Name = "Contenido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(100)")]
        public string? Content { get; set; }

        [Display(Name = "Tipo de Publicación")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(minimum:10,maximum:20,ErrorMessage ="Elija una opción valida")]
        public PublicationType PublicationType { get; set; }

        [Display(Name = "Fecha de Publicación")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublicationDate { get; set; }

        [Display(Name = "Precio Estimado")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(100)")]
        [RegularExpression(pattern: "^\\d{1,}(?:([.,])\\d{1,})*(?:\\1\\d{2})?$",ErrorMessage ="Formato erroneo")]
        public string? EstimatedPrice { get; set; }

        [Display(Name = "Descripción del producto")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(100)")]
        public string? ProductDescription { get; set; }


        [Display(Name = "Categoría")]
        public Guid CategoryID { get; set; }

        public bool IsActive { get; set; }


        public bool IsCalificationPerson { get; set; }


        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Status Status { get; set; }

        [JsonIgnore]
        public virtual ICollection<PublicationAttachment>? PublicationAttachment { get; set; }


        [JsonIgnore]
        public virtual ICollection<PublicationComments>? PublicationComments { get; set; }

        [JsonIgnore]
        public virtual Person? Person { get; set; }


		[JsonIgnore]
		public virtual Category? Category { get; set; }
	}
}
