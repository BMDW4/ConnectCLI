using WmdaConnect.Models.MessageBases;
using WmdaConnect.Models.MessagePayloads;

namespace WmdaConnect.Models
{
    public class Warning : Message<WarningPayload>
    {
        public Warning(MessageRequest<WarningPayload> messageRequest) : base(messageRequest)
        {
        }
    }
}
