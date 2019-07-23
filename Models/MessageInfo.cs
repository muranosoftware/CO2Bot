using Microsoft.Bot.Schema;

namespace EchoBot.Models {
	public class MessageInfo {
		public string Text;
		public string UserName;
		public string UserId;
		public string ConversationId;
		public bool? IsGroup;
	}
}