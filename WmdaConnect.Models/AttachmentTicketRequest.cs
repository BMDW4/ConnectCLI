using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models
{
    public class AttachmentTicketRequest
    {
        [Required]
        public string FileName { get; set; }

    }

}
