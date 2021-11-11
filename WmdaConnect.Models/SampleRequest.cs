using WmdaConnect.Models.MessageBases;
using WmdaConnect.Models.MessagePayloads;

namespace WmdaConnect.Models
{
    public class SampleRequest : Message<SampleRequestPayload>
    {
        public SampleRequest(MessageRequest<SampleRequestPayload> messageRequest) : base(messageRequest)
        {
        }
    }
}
