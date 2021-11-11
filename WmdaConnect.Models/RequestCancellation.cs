using WmdaConnect.Models.MessageBases;
using WmdaConnect.Models.MessagePayloads;

namespace WmdaConnect.Models
{
    public class RequestCancellation : Message<RequestCancellationPayload>
    {
        public RequestCancellation(MessageRequest<RequestCancellationPayload> messageRequest) : base(messageRequest)
        {
        }
    }
}
