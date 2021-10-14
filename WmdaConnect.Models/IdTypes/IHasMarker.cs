using System.ComponentModel.DataAnnotations;
using WmdaConnect.Models.FieldDictionary;

namespace WmdaConnect.Models.IdTypes
{
    //Attributes here have no effect on swagger, for reference only
    internal interface IHasMarker
    {
        /// <summary>
        /// Infectious disease markers as requested (IDM_REQ) / Infectious disease markers as performed SMP_REQ) MARKER Req 19
        /// </summary>
        [MinLength(MarkerField.MinLength)]
        [MaxLength(MarkerField.MaxLength)]
        public string Marker { get; set; }
    }
}
