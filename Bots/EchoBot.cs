using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EchoBot.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace EchoBot.Bots {
	public class EchoBot : ActivityHandler {
		private readonly IHubContext<ProxyHub> _hubContext;

		public EchoBot(IHubContext<ProxyHub> hubContext) {
			_hubContext = hubContext;
		}

		protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken) {
			string message = turnContext.Activity.Text;
			string userName = turnContext.Activity.From.Name;
			await turnContext.SendActivityAsync(MessageFactory.Text($"Command from {userName}: {message}"), cancellationToken);
			await _hubContext.Clients.All.SendCoreAsync("ReceiveMessage", new object[] { userName, message }, cancellationToken);
		}

		protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken) {
			foreach (ChannelAccount member in membersAdded) {
				if (member.Id != turnContext.Activity.Recipient.Id) {
					await turnContext.SendActivityAsync(MessageFactory.Text("Hello and welcome!"), cancellationToken);
				}
			}
		}
	}
}