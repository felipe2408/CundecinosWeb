﻿using CundecinosWeb.Data;
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

        //public async Task SendMessage(string user,string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage",user,message);
        //}

        //public async Task SendMessageUser(Guid sender, Guid addressee , string message)
        //{

        //    // Guardar el mensaje en la base de datos
        //    var chatMessage = new Message
        //    {
        //        Text = message,
        //        SentAt = DateTime.Now,
        //        Sender = sender,
        //        Addressee = addressee
        //    };
        //    _context.Messages.Add(chatMessage);
        //    await _dbContext.SaveChangesAsync();

        //    // Enviar el mensaje a través de SignalR
        //    await Clients.User(user).SendAsync("ReceiveMessage", addressee, message);
        //}

        public async Task SendMessage(string user, string message)
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
            await Clients.User(user).SendAsync("ReceiveMessage", user, message);
        }


    }
}