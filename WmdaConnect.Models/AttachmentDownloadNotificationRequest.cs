using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models
{
    public class AttachmentDownloadNotificationRequest
    {
        [Required]
        public System.Guid AttachmentGuid { get; set; }

    }

}
