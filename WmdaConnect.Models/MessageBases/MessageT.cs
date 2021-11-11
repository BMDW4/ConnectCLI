using System;
using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models.MessageBases
{
    public abstract class Message<T> :Message
    {
        [Required]
        public T Payload { get; set; }
    }
}
