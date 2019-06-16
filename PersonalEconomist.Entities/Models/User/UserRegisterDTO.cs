using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using PersonalEconomist.Entities.Models.Base;

namespace PersonalEconomist.Entities.Models.User
{
    public class UserRegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        public double? Amount { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
