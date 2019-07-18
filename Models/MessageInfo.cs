using Microsoft.Bot.Schema;

namespace EchoBot.Models {
	public class MessageInfo {
		public string Text;
		public string UserName;
		public string ChanelId;
		public dynamic ChanelData;
		public ConversationAccount Conversation;
		public ChannelAccount From;
	}
}