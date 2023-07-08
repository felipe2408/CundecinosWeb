using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CundecinosWeb.Models
{
    public class CommentAttachment
    {

        public Guid CommentAttachmentID { get; set; }

        public Guid PublicationCommentsID { get; set; }


        [Display(Name = "Imagen  full")]
        [Required(ErrorMessage = "The {0} is required")]
        public string? ImageScreen { get; set; }

        [Display(Name = "Imagen Pantalla")]
        [Required(ErrorMessage = "The {0} is required")]
        public string? ImageThumbNail { get; set; }

        [Display(Name = "Creado")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Created { get; set; }

        [Display(Name = "Creado por")]
        [Column(TypeName = "varchar(200)")]
        [StringLength(200)]
        public string? CreatedBy { get; set; }

        [JsonIgnore]
        public virtual PublicationComments? PublicationComments { get; set; }
    }
}
