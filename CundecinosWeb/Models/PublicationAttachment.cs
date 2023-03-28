using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CundecinosWeb.Models
{
    public class PublicationAttachment
    {
        [Key]
        public Guid PublicationAttachmentID { get; set; }

        public Guid PublicationID { get; set; }

        [Display(Name = "Nombre de Archivo")]
        [Required(ErrorMessage = "The {0} is required")]
        public string? FileName { get; set; }

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
        public byte[]? File { get; set; }


        [JsonIgnore]
        public virtual Publication? Publication { get; set; }

    }
}
