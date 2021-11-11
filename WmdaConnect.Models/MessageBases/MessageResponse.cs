using System;

namespace WmdaConnect.Models.MessageBases
{
    public class MessageResponse
    {
        public MessageResponse(Message message)
        {
            SentAtUtc = message.SentAtUtc;
            DeliveredAtUtc = message.DeliveredAtUtc;
        }
        /// <summary>
        /// Server-supplied timestamp showing when sender posted (/sent) MessageRequest
        /// </summary>
        public DateTime SentAtUtc { get; }

        /// <summary>
        /// Server-supplied timestamp showing time of Message delivery to recipient's inbox queue
        /// </summary>
        public DateTime DeliveredAtUtc { get; }
    }
}
