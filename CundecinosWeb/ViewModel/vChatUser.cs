using CundecinosWeb.Models;

namespace CundecinosWeb.ViewModel
{
    public class vChatUser
    {
        public Guid UID { get; set; }
        public Person Person { get; set; }
        public List<Person> People { get; set; }
    }
}
