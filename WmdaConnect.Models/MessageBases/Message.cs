using System;
using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models.MessageBases
{
    /// <summary>
    /// Based class for Messages (information placed on queue)
    /// </summary>
    public abstract class Message : MessageRequest, IMessage
    {
        /// <summary>
        /// 4 digit ION of sender
        /// </summary>
        /// <example>5678</example>
        [Required]
        [MinLength(4)]
        [MaxLength(4)]
        [Range(typeof(string), "0000", "9999")]
        public string Sender { get; set; }

        /// <summary>
        /// Server-supplied timestamp showing UTC time sender posted (i.e. sent) MessageRequest.
        /// </summary>
        [Required]
        public DateTime SentAtUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Server-supplied timestamp showing UTC time of Message delivery to recipient's inbox queue.
        /// </summary>
        [Required]
        public DateTime DeliveredAtUtc { get; set; } = DateTime.UtcNow;
    }
}
