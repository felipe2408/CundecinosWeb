using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CundecinosWeb.Models
{
    public class PublicationComments
    {
        [Key]
        public Guid PublicationCommentsID { get; set; }

        public Guid PersonID { get; set; }

        [Display(Name = "Publicación ID")]
        public Guid PublicationID { get; set; }

        [Display(Name = "Comentario")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(100)")]
        public string? Content { get; set; }

        [Display(Name = "Fecha de Comentario")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CommentDate { get; set; }

        [Display(Name = "Imagen del Producto")]
        [Column(TypeName = "varchar(100)")]
        public string? ProductUrl { get; set; }


        [JsonIgnore]
        public virtual Publication? Publication { get; set; }

        [JsonIgnore]
        public virtual Person? Person { get; set; }



    }
}
