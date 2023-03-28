using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CundecinosWeb.Models
{
    public class PublicationComments
    {
        [Key]
        public Guid PublicationCommentsID { get; set; }

        [Display(Name = "Publicación ID")]
        public Guid PublicationID { get; set; }

        [Display(Name = "Contenido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(100)")]
        public string? Content { get; set; }

        [Display(Name = "Fecha de Comentario")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublicationDate { get; set; }
    }
}
