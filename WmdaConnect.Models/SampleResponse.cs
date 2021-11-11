using WmdaConnect.Models.MessageBases;
using WmdaConnect.Models.MessagePayloads;

namespace WmdaConnect.Models
{
    public class SampleResponse : Message<SampleResponsePayload>
    {
        public SampleResponse(MessageRequest<SampleResponsePayload> messageRequest) : base(messageRequest)
        {
        }
    }
}
