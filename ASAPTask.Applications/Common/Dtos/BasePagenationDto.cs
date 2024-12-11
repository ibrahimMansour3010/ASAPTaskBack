using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Common.Dtos
{
    public class BasePagenationDto
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; } = 1;
    }
}
