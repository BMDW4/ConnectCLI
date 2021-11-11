using WmdaConnect.Models.MessageBases;
using WmdaConnect.Models.MessagePayloads;

namespace WmdaConnect.Models;

public class InfectiousDiseaseMarkerRequest : Message<InfectiousDiseaseMarkerRequestPayload>
{
    public static explicit operator InfectiousDiseaseMarkerRequest(InfectiousDiseaseMarkerRequestRequest messageRequest)
    {
        return new InfectiousDiseaseMarkerRequest()
        {
            Recipient = messageRequest.Recipient,
            CorrelationGuid = messageRequest.CorrelationGuid,
            Payload = messageRequest.Payload
        };
    }
}