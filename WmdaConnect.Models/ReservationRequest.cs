using WmdaConnect.Models.MessageBases;
using WmdaConnect.Models.MessagePayloads;

namespace WmdaConnect.Models
{
    public class ReservationRequest : Message<ReservationRequestPayload>
    {
        public ReservationRequest(MessageRequest<ReservationRequestPayload> messageRequest) : base(messageRequest)
        {
        }
    }
}
