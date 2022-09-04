using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.UserDtos
{
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage = "Bu alan gereklidir")]
        [EmailAddress]
        public string Email { get; set; }

        public bool Success { get; set; } = false;
    }
}
