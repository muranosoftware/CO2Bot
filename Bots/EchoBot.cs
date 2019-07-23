using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EchoBot.Hubs;
using EchoBot.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;

namespace EchoBot.Bots {
	public class EchoBot : ActivityHandler {
		private readonly IHubContext<ProxyHub> _hubContext;

		public EchoBot(IHubContext<ProxyHub> hubContext) {
			_hubContext = hubContext;
		}

		protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken) {
			var messageInfo = new MessageInfo {
				Text = turnContext.Activity.Text,
				UserName = turnContext.Activity.From.Name,
				UserId = turnContext.Activity.From.Id,
				ConversationId = turnContext.Activity.Conversation.Id,
				IsGroup = turnContext.Activity.Conversation.IsGroup
			};

			//Activity reply = MessageFactory.Text($"Command from {messageInfo.UserName}: {messageInfo.Text}");
			//await turnContext.SendActivityAsync(reply, cancellationToken);
			await _hubContext.Clients.All.SendCoreAsync("ReceiveMessage", new object[] { messageInfo.UserName, JsonConvert.SerializeObject(messageInfo) }, cancellationToken);
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