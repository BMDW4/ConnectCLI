using WmdaConnect.Models.MessageBases;
using WmdaConnect.Models.MessagePayloads;

namespace WmdaConnect.Models
{
    public class MessageAcknowledgement : Message<MessageAcknowledgementPayload>
    {
        public MessageAcknowledgement(MessageRequest<MessageAcknowledgementPayload> messageRequest) : base(messageRequest)
        {
        }
    }
}
