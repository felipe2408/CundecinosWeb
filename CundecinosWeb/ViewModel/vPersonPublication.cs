using CundecinosWeb.Models;

namespace CundecinosWeb.ViewModel
{
    public class vPersonPublication
    {

        public Person? Person { get; set; }

        public Publication? Publication { get; set; }

        public List<Publication>? PublicationUsers { get; set; }
    }
}
