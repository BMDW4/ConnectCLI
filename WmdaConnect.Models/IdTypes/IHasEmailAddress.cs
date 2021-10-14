using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;

namespace WmdaConnect.Models.IdTypes
{
    //Attributes here have no effect on swagger, for reference only
    internal interface IHasEmailAddress
    {
        /// <summary>
        /// Email address EMAIL Req 60
        /// </summary>
        [Required]
        [MaxLength(EmailAddressField.MaxLength)]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
