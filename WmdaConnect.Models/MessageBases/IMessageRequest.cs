using System;

namespace WmdaConnect.Models.MessageBases
{
    public interface IMessageRequest
    {
        string Recipient { get; set; }
        Guid CorrelationGuid { get; set; }
        MessageTypes MessageType { get; }
    }
}
