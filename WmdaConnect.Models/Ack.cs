using WmdaConnect.Models.MessageBases;

namespace WmdaConnect.Models
{
    public class Ack : Message
    {
        public Ack(MessageRequest messageRequest) : base(messageRequest)
        {

        }
    }
}
