using System;

namespace WmdaConnect.Models.MessageBases
{
    public interface IMessage : IMessageRequest
    {
        public string Sender { get; set; }

        public DateTime SentAtUtc { get; set; }

        public DateTime DeliveredAtUtc { get; set; }
    }
}
