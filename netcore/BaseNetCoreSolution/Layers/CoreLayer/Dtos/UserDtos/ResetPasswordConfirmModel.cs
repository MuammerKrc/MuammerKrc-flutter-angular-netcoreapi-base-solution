using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.UserDtos
{
    public class ResetPasswordConfirmDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public bool Success { get; set; } = false;

        [DataType(DataType.Password)]
        public string PasswordNew { get; set; }
    }
}
