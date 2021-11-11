using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models.MessageBases;

public abstract class Message<T> : Message
{
    protected Message()
    {
    }

    protected Message(MessageRequest<T> messageRequest)
    {
        Recipient = messageRequest.Recipient;
        CorrelationGuid = messageRequest.CorrelationGuid;
        Payload = messageRequest.Payload;
    }

    [Required]
    public T Payload { get; set; }
}