using System;
using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models.MessageBases
{
    public abstract class Message<T> : MessageRequest<T>, IMessage
    {
        /// <summary>
        /// 4 digit ION of sender
        /// </summary>
        /// <example>5678</example>
        [Required]
        [MinLength(4)]
        [MaxLength(4)]
        public string Sender { get; set; }

        /// <summary>
        /// Server-supplied timestamp showing UTC time sender posted (i.e. sent) MessageRequest.
        /// </summary>
        [Required]
        public DateTime SentAtUtc { get; set; }

        /// <summary>
        /// Server-supplied timestamp showing UTC time of Message delivery to recipient's inbox queue.
        /// </summary>
        [Required]
        public DateTime DeliveredAtUtc { get; set; }
    }
}
