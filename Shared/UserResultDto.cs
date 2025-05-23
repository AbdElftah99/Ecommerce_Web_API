using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record UserResultDto(string userId, string DisplayName, string Email, string Token);
}
