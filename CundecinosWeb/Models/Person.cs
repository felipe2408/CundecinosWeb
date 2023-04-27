using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace CundecinosWeb.Models
{
    public class Person
    {
        [Key]
        public Guid PersonID { get; set; }

        public Guid? UID { get; set; }

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(100)")]
        public String? FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(100)")]
        public String? LastName { get; set; }

        [Display(Name = "Documento Identificación")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(20)")]
        public String? IdentificationNumber { get; set; }


        [Display(Name = "Número Celular")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(20)")]
        public String? CellPhone { get; set; }


        [Display(Name = "Correo Electrónico")]
        [Column(TypeName = "varchar(100)")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "El valor de {0} no es un correo válido")]
        public string? Email { get; set; }

        [Display(Name = "Año de Nacimiento")]
        //[Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1940, 2010, ErrorMessage = "El valor ingresado no es permitido")]
        public Int16? BirthYear { get; set; }

        [Display(Name = "Extensión Universitaria")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? ExtensionId { get; set; }

        [Display(Name = "Carrera")]
        //[Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? CollegeCareerId { get; set; }


        [Display(Name = "Avatar")]
        [Column(TypeName = "varchar(100)")]
        public string? AvatarUrl { get; set; }

        [Display(Name = "Registro vigente?")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public bool IsActive { get; set; }


        [JsonIgnore]
        public virtual CollegeCareer? CollegeCareer { get; set; }


        [JsonIgnore]
        public virtual ICollection<Publication>? Publication { get; set; }


        [JsonIgnore]
        public virtual ICollection<PublicationComments>? PublicationComments { get; set; }

        [JsonIgnore]
        public virtual Extension? Extension { get; set; }
    }
}
