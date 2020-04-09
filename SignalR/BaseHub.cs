using System;
using System.Linq;
using System.Threading.Tasks;
using CFT.Models;
using CFT.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CFT.SignalR
{
    [Authorize]
    public class BaseHub : Hub
    {
        protected readonly CftDbContext DbContext;
        protected static readonly ConnectionMapping<int> Connections = new ConnectionMapping<int>();

        public BaseHub(CftDbContext dbContext)
        {
            DbContext = dbContext;
        }
        
        public override async Task OnConnectedAsync()
        {
            Connections.Add(GetCurrentUserId(), Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
			Connections.Remove(GetCurrentUserId(), Context.ConnectionId);
			await base.OnDisconnectedAsync(ex);
		}

        private int GetCurrentUserId()
        {
	        if (int.TryParse(Context.User.Identity.Name, out int userId)) {
               return userId;
            }

	        throw new Exception("Не удалось распарсить User ID");
        }

        protected User GetCurrentUser() => DbContext.Users.FirstOrDefault(u => u.Id == GetCurrentUserId());
    }
}