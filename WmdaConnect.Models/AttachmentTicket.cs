using System;
using System.Collections.Generic;
using System.Text;

namespace WmdaConnect.Models
{
    public class AttachmentTicketResponse
    {
        public Guid AttachmentGuid { get; set; }
        public string AttachmentUploadUrl { get; set; }
    }
}
