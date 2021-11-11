using WmdaConnect.Models.MessageBases;
using WmdaConnect.Models.MessagePayloads;

namespace WmdaConnect.Models
{
    public class TypingRequest : Message<TypingRequestPayload>
    {
        public TypingRequest(MessageRequest<TypingRequestPayload> messageRequest) : base(messageRequest)
        {
        }
    }
}
