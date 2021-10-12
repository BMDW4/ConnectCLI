using System;
using System.ComponentModel.DataAnnotations;

namespace WmdaConnect.Models.IdTypes
{
    //Attributes here have no effect on swagger, for reference only
    internal interface IHasExpirationDate
    {
        /// <summary>
        /// Request date EXPI_DATE Req 8 yyyy-MM-dd [or yyyyMMdd]
        /// </summary>
        [Required]
        [Range(typeof(DateTime), "02-Jan-0001", "31-Dec-9999", ErrorMessage = "Required, yyyy-MM-dd [or yyyyMMdd]")]
        public DateTime? ExpirationDate { get; set; }
    }
}
