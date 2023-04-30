using CundecinosWeb.Models;

namespace CundecinosWeb.ViewModel
{
    public class vChatUser
    {
        public Person Person { get; set; }
        public List<Person> People { get; set; }
        public List<Message> SentMessages { get; set; }
        public List<Message> ReceivedMessages { get; set; }
    }
}
