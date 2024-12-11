using ASAPTask.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Common.Interfaces
{
    public interface IUserHandler
    {
        Task<string> GenerateJwtAsync(ApplicationUser user, DateTime expires, CancellationToken cancellationToken);
        string GenerateRefreshToken();
    }
}
