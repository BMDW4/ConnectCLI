using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;

namespace WmdaConnect.Models.IdTypes
{
    //Attributes here have no effect on swagger, for reference only
    internal interface IHasAcknowledgementId
    {
        /// <summary>
        /// Acknowledgement ID ACK_ID Req 17
        /// </summary>
        [Required]
        [MaxLength(AcknowledgementIdField.MaxLength)]
        public string AcknowledgementId { get; set; }
    }
}
