using System;

namespace WmdaConnect.Models.MessageBases
{
    public interface IMessage
    {
        public string Sender { get; set; }

        public MessageTypes MessageType { get; }

        public DateTime SentAtUtc { get; set; }

        public DateTime DeliveredAtUtc { get; set; }
    }
}
