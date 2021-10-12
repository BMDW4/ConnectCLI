using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;

namespace WmdaConnect.Models.IdTypes
{
    //Attributes here have no effect on swagger, for reference only
    internal interface IHasInstitutionPaying
    {
        /// <summary>
        /// Institution paying INST_PAY Req 10
        /// </summary>
        [Required]
        [MaxLength(InstitutionPayingField.MaxLength)]
        public string InstitutionPaying { get; set; }
    }
}
