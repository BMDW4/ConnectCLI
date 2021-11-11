using WmdaConnect.Models.MessageBases;
using WmdaConnect.Models.MessagePayloads;

namespace WmdaConnect.Models
{
    public class TextMessage : Message<TextMessagePayload>
    {
        public TextMessage(MessageRequest<TextMessagePayload> messageRequest) : base(messageRequest)
        {
        }
    }
}
