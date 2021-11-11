using WmdaConnect.Models.MessageBases;
using WmdaConnect.Models.MessagePayloads;

namespace WmdaConnect.Models
{
    public class NoResult : Message<NoResultPayload>
    {
        public NoResult(MessageRequest<NoResultPayload> messageRequest) : base(messageRequest)
        {
        }
    }
}
