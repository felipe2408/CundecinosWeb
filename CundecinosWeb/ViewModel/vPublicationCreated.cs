using CundecinosWeb.Models;

namespace CundecinosWeb.ViewModel
{
	public class vPublicationCreated
	{
		public Guid PublicationID { get; set; }

		public Guid PersonID { get; set; }

		public string AvatarURL { get; set; }

		public Publication Publication { get; set; }
	}
}
