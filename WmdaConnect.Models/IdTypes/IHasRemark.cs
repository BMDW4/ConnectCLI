using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;

namespace WmdaConnect.Models.IdTypes
{
    //Attributes here have no effect on swagger, for reference only
    internal interface IHasRemark
    {
        /// <summary>
        /// Remark REMARK Opt 120
        /// </summary>
        [MaxLength(RemarkField.MaxLength)]
        public string Remark { get; set; }
    }
}
