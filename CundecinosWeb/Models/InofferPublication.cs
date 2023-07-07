using CundecinosWeb.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CundecinosWeb.Models
{
    public class InofferPublication
    {
        [Key]
        public Guid InofferPublicationID { get; set; }

        public StatusInnofer StatusInnofer { get; set; }

        public Guid PublicationID { get; set; }

        public Guid PersonID { get; set; }


        [Display(Name = "Descripción de Oferta")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(100)")]
        public string? Offer { get; set; }

        [Display(Name = "Fecha de Negociación")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [JsonIgnore]
        public virtual Person? Person { get; set; }

        //[JsonIgnore]
        //[ForeignKey("PublicationID")]
        //[InverseProperty("InofferPublications")]
        //public virtual Publication? Publication { get; set; }
    }
}
