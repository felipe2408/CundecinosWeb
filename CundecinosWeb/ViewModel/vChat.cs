using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CundecinosWeb.Models;

namespace CundecinosWeb.ViewModel
{
    public class vChat
    {
        public List<Person> People { get; set; }
        public List<Person> PeopleWritten { get; set; } 

    }
}
