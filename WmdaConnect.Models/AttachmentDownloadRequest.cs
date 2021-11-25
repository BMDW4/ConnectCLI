using System;
using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models
{
    public class AttachmentDownloadRequest
    {
        [Required]
        public Guid AttachmentGuid { get; set; }

    }

}
