using System.ComponentModel.DataAnnotations;

namespace CundecinosWeb.Models
{
    public class Message
    {
        public Guid MessageID { get; set; }

        [Display(Name = "Mensaje")]
        public string? Text { get; set; }

        [Display(Name = "Enviado A Las")]
        public DateTime SentAt { get; set; }


        [Display(Name = "Remitente")]
        public Guid Sender { get; set; }

        [Display(Name = "Remitente")]
        public Guid Addressee { get; set; }


        public virtual Person Person { get; set; }
    }
}
