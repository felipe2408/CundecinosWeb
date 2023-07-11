using CundecinosWeb.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CundecinosWeb.Models
{
    public class CalificationPerson
    {

        [Key]
        public Guid CalificationPersonID { get; set; }

        [Display(Name = "Calificación")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(100)")]
        public Calification Calification { get; set; }


        [Display(Name = "Contenido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(100)")]
        public string? Observations { get; set; }

        [Display(Name = "Fecha de Calificación")]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CalificationDate { get; set; }

        public Guid PersonID { get; set; }


        [JsonIgnore]
        public virtual Person? Person { get; set; }
    }
}
