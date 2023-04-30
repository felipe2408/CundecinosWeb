using CundecinosWeb.Models;

namespace CundecinosWeb.ViewModel
{
    public class vMessageChat
    {

        public Guid SenderUID { get; set; }

        public Guid AddresseeUID { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }


        public Person Person { get; set; }
        public List<Person> People { get; set; }


    }
}
