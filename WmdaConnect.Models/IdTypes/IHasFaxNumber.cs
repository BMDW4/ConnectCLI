using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;

namespace WmdaConnect.Models.IdTypes
{
    //Attributes here have no effect on swagger, for reference only
    internal interface IHasFaxNumber
    {
        /// <summary>
        /// Fax Number FAX Req 20
        /// </summary>
        [Required]
        [MaxLength(FaxNumberField.MaxLength)]
        public string FaxNumber { get; set; }
    }
}
