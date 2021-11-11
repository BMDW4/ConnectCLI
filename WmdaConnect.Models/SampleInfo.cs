using WmdaConnect.Models.MessageBases;
using WmdaConnect.Models.MessagePayloads;

namespace WmdaConnect.Models
{
    public class SampleInfo : Message<SampleInfoPayload>
    {
        public SampleInfo(MessageRequest<SampleInfoPayload> messageRequest) : base(messageRequest)
        {
        }
    }
}
