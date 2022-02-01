using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.HubConfig
{
    public class NotificationHub:Hub
    {
        public string AddToGroup(string groupName)
        { 
            Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            return Context.ConnectionId;
        }

        public async Task RemoveFromGroup(string groupName)
            => await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        public string GetConnectionId() => Context.ConnectionId;

        public async Task BroadcastToGroup(string groupName)

        => await Clients.GroupExcept(groupName,Context.ConnectionId).SendAsync("broadcasttogroup", $"Data Updated. Please Refresh.");
        public async Task SendNotification(string groupName)

        => await Clients.Group(groupName).SendAsync("sendnotification", $"Notification Received.");

    }

    public interface INotificationHubService
    {
        Task NotificationReceived(string message);
        Task DataUpdated(string message);


    }
}
