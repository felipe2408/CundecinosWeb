using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CundecinosWeb.Models
{
    public class Message
    {
        public Guid MessageID { get; set; }

        [Display(Name = "Mensaje")]
        public string Text { get; set; }

        [Display(Name = "Enviado A Las")]
        public DateTime SentAt { get; set; }

        public Guid SenderID { get; set; }

        
        public Guid AddresseeID { get; set; }


        [Display(Name = "Remitente")]
        public virtual Person Sender { get; set; }

        [Display(Name = "Destinatario")]
        public virtual Person Addressee { get; set; }

    }
}
