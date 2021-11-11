using WmdaConnect.Models.MessageBases;

namespace WmdaConnect.Models
{
    public class Ping : Message
    {
        public Ping(MessageRequest messageRequest) : base(messageRequest)
        {

        }
    }
}
