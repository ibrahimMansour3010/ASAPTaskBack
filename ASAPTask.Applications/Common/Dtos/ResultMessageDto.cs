using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Common.Dtos
{
    public class ResultMessageDto
    {
        public int? ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
