using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.Domain.Core.Entites.BaseEntities;

public class Result
{
    public string Message { get; set; }
    public bool Flag { get; set; }

    public Result(string message, bool flag)
    {
        Message = message;
        Flag = flag;
    }
}
