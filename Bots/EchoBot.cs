﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

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

		private EchoBot(IHubContext<ProxyHub> hubContext) {
			_hubContext = hubContext;
		}

		protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken) {
			string message = turnContext.Activity.Text;
			string user = turnContext.Activity.Recipient.Id;
			await turnContext.SendActivityAsync(MessageFactory.Text($"From {user}: {message}"), cancellationToken);
			await _hubContext.Clients.All.SendCoreAsync("ReceiveMessage", new object[] { user, message }, cancellationToken);
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