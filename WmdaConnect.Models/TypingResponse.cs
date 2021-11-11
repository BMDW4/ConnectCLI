using WmdaConnect.Models.MessageBases;
using WmdaConnect.Models.MessagePayloads;

namespace WmdaConnect.Models
{
    public class TypingResponse : Message<TypingResponsePayload>
    {
        public TypingResponse(MessageRequest<TypingResponsePayload> messageRequest) : base(messageRequest)
        {
        }
    }
}
