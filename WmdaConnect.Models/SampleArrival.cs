using WmdaConnect.Models.MessageBases;
using WmdaConnect.Models.MessagePayloads;

namespace WmdaConnect.Models
{
    public class SampleArrival : Message<SampleArrivalPayload>
    {
        public SampleArrival(MessageRequest<SampleArrivalPayload> messageRequest) : base(messageRequest)
        {
        }
    }
}
