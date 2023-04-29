using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CundecinosWeb.Models
{
    public class NotificationMessage
    {

        public Guid NotificationMessageID { get; set; }


        [Display(Name = "Contenido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Column(TypeName = "varchar(100)")]
        public string Content { get; set; }


        [Display(Name = "Remitente")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid SenderID { get; set; }



        [Display(Name = "Destinatario")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid AddresseeID { get; set; }

        [Display(Name = "Ver Notificación?")]
        [Required(ErrorMessage = "El campo {0} es requerido")]

        public bool View { get; set; }


        

    }
}
