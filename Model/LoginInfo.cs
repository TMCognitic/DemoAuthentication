using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class LoginInfo
    {
        [Required]
        [EmailAddress]
        [MaxLength(320)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(20)]
        public string Passwd { get; set; }
    }
}
