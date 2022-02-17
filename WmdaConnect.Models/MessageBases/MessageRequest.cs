using System;
using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models.MessageBases
{
    public abstract class MessageRequest : IMessageRequest
    {
        protected MessageRequest()
        {
            var messageTypeString = this.GetType().Name;
            if (this is not Message && this.GetType().BaseType?.Name != "Message`1")
            {
                messageTypeString = messageTypeString[..^7];
            }
            MessageType = (MessageTypes)Enum.Parse(typeof(MessageTypes), messageTypeString);

        }
        /// <summary>
        /// 4 digit ION of recipient
        /// </summary>
        /// <example>1234</example>
        [Required]
        [MinLength(4)]
        [MaxLength(4)]
        [Range(typeof(string), "0000", "9999")]
        public string Recipient { get; set; }

        /// <summary>
        /// Sender generated GUID used to correlate response acknowledgement
        /// </summary>
        [Required]
        public Guid CorrelationGuid { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Message Type
        /// </summary>
        /// <example>[example always set to first element of enum]</example>
        [SwaggerIgnoreProperty]
        public MessageTypes MessageType { get; }
    }
}
