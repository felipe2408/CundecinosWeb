using CundecinosWeb.Models;

namespace CundecinosWeb.ViewModel
{
	public class vPublicationComment
	{

		public Person? Person { get; set; }

		public Publication? Publication { get; set; }

		public List<PublicationComments>? PublicationComments { get; set; }

		public PublicationComments? PublicationComment { get; set; }



    }
}
