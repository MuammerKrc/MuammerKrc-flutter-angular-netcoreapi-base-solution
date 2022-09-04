using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Exceptions
{
    public class TokenException : Exception
    {
        public TokenException() : base("Kullanıcı ve şifre eşleşmedi")
        {

        }
        public TokenException(string message) : base(message)
        {

        }
    }
}
