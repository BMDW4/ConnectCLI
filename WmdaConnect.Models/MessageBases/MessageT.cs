using System;
using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models.MessageBases
{
    public abstract class Message<T> : Message
    {
        public Message(MessageRequest<T> messageRequest) : base(messageRequest)
        {
            Payload = messageRequest.Payload;
        }

        [Required]
        public T Payload { get; }
    }
}
