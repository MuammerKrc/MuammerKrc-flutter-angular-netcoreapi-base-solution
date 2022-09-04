using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dtos.UserDtos
{
    public class UserLoginDto
    {
        public string EmailOrUserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool IsClient { get; set; } = false;
    }
}
