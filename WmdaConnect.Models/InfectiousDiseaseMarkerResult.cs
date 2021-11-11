using WmdaConnect.Models.MessageBases;
using WmdaConnect.Models.MessagePayloads;

namespace WmdaConnect.Models
{
    public class InfectiousDiseaseMarkerResult : Message<InfectiousDiseaseMarkerResultPayload>
    {
        public InfectiousDiseaseMarkerResult(MessageRequest<InfectiousDiseaseMarkerResultPayload> messageRequest) : base(messageRequest)
        {
        }
    }
}
