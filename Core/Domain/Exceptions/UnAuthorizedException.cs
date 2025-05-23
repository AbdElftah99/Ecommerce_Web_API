using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException(string message = "Invalid Username or password") : base(message) 
        {
            
        }
    }
}
