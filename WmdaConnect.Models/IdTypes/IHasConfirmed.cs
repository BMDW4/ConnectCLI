using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models.IdTypes
{
    //Attributes here have no effect on swagger, for reference only
    internal interface IHasConfirmed
    {
        /// <summary>
        /// Confirmation of reservation CONFIRM Req 1
        /// </summary>
        [Required]
        public bool Confirmed { get; set; }
    }
}
