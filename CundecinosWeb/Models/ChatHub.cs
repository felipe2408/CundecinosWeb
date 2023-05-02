using CundecinosWeb.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace CundecinosWeb.Models
{
    public class ChatHub : Hub
    {
        private readonly DataContext _context;
        public ChatHub(DataContext context)
        {
            _context = context;

        }


        //public async Task SendMessage(string user, string message)=> await Clients.User(user).SendAsync("ReceiveMessage", user, message);
        //public async Task SendMessage(Message message) => await Clients.User(message.AddresseeID.ToString()).SendAsync("receiveMessage", message);


        public async Task SendMessage(string user, string sender,string message)
        {
            // Guardar el mensaje en la base de datos
            //var chatMessage = new ChatMessage
            //{
            //    Mensaje = message,
            //    FechaHora = DateTime.Now,
            //    Remitente = Context.User.Identity.Name,
            //    Destinatario = user
            //};
            //_dbContext.ChatMessages.Add(chatMessage);
            //await _dbContext.SaveChangesAsync();

            // Enviar el mensaje a través de SignalR
           await Clients.User(user).SendAsync("ReceiveMessage", user,sender, message);
        }
    }
}
