﻿using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models.MessageBases
{
    public abstract class MessageRequest<T> : MessageRequest
    {
        [Required]
        public T Payload { get; set; }

    }
}
