using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Entites.DTOs;

public class UserLoginDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsPresistent { get; set; }
    public string Role { get; set; }
}
