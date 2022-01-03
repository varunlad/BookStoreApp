using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserModel
{
    public class SignupModel
    {
        [Key]
        [Required]
        public int userId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Int64 PhoneNumber { get; set; }

    
}
}
