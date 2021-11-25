using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WmdaConnect.Models
{
    public class AttachmentTicketRequest
    {
        [Required]
        public string FileName { get; set; }

    }

}
