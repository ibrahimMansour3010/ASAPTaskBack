using ASAPTask.Applications.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Common.Interfaces
{
    public interface IResult
    {
        IList<ResultMessageDto> Messages { get; set; }
        bool Succeeded { get; set; }
    }

    public interface IResult<out T> : IResult
    {
        T Data { get; }
    }
}
