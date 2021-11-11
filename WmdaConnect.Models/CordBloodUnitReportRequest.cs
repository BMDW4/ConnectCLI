using WmdaConnect.Models.MessageBases;
using WmdaConnect.Models.MessagePayloads;

namespace WmdaConnect.Models
{
    public class CordBloodUnitReportRequest : Message<CordBloodUnitReportRequestPayload>
    {
        public CordBloodUnitReportRequest(MessageRequest<CordBloodUnitReportRequestPayload> messageRequest) : base(messageRequest)
        {
        }
    }
}
