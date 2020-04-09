using Microsoft.AspNetCore.SignalR;
using CFT.Models;
using System;
using System.Threading.Tasks;
using CFT.Models.Chat;

namespace CFT.SignalR
{
	public class ChatHub : BaseHub
	{
        public async Task Send(string message)
        {
            var user = GetCurrentUser();

            DbContext.Messages.Add(new Message()
            {
                AuthorId = user.Id,
                Text = message,
                CreatedDate = DateTime.UtcNow,
                IsReaded = false
            });

            DbContext.SaveChanges();
            await Clients.All.SendAsync("Send", message, user.GetFullName(), DateTime.UtcNow.ToString("dd/MM hh:mm"), "data:image/jpeg;base64," + Convert.ToBase64String(user.Avatar));
        }

        public ChatHub(CftDbContext dbContext) : base(dbContext)
        {
        }
	}
}
