using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Common.Exceptions
{
    public class BusinessException : Exception
    {
        public IList<string> Keys { get; set; }
        public BusinessException() : base("Error occurred.")
        {
            Keys = new List<string>();
        }
        public BusinessException(string key) : this()
        {
            Keys.Add(key);
        }
        public BusinessException(IList<string> keys) : this()
        {
            Keys = keys;
        }
    }
}
