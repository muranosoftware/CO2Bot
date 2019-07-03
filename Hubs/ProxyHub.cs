using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace EchoBot.Hubs {
	public class ProxyHub : Hub {
		public async Task SendMessage(string user, string message) {
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}
	}
}