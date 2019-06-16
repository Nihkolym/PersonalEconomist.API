using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using PersonalEconomist.Entities.Models.Base;

namespace PersonalEconomist.Entities.Models.CreditCard
{
    public class CreditCardDTO : BaseGuidDTOEntity
    {
        [Required]
        public double Amount { get; set; }
        [Required]
        public string PinCode { get; set; }
        [Required]
        public string CardNumber { get; set; }
    }
}
