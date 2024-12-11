using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Common.Exceptions
{
    public class ValidationException:Exception
    {
        public IList<string> Keys { get; }

        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Keys = new List<string>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            var propertyNames = failures.Select(e => e.PropertyName).Distinct();

            foreach (var propertyName in propertyNames.ToList())
            {
                var errorCodes = failures.Where(e => e.PropertyName == propertyName).Select(e => e.ErrorCode).Distinct();

                foreach (var errorCode in errorCodes)
                {
                    Keys.Add(errorCode);
                }
            }
        }
    }
}
