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


        public async Task SendMessage(string user, string message)=> await Clients.User(user).SendAsync("ReceiveMessage", user, message);

    }
}
