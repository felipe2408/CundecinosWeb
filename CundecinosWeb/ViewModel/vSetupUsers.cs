using CundecinosWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CundecinosWeb.ViewModel
{
	public class vSetupUsers
	{
        public List<Person> Users{ get; set; }
        public SelectList CollegeCareers { get; set; }
        public SelectList Extensions { get; set; }
    }
}
