using ASAPTask.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Common.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepo<T, TType> Repository<T, TType>() where TType : struct where T : BaseEntity<TType> ;
        void Complete();
        Task<bool> CompleteAsync();
    }
}
