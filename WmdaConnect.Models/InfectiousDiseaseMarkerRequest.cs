using WmdaConnect.Models.MessageBases;
using WmdaConnect.Models.MessagePayloads;

namespace WmdaConnect.Models
{
    public class InfectiousDiseaseMarkerRequest : Message<InfectiousDiseaseMarkerRequestPayload>
    {
        public InfectiousDiseaseMarkerRequest(MessageRequest<InfectiousDiseaseMarkerRequestPayload> messageRequest) : base(messageRequest)
        {
        }
    }
}
