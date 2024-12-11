using ASAPTask.SharedKernal.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.SharedKernal.Services
{
    public class CurrentUserService
    {
        public readonly IHttpContextAccessor _context;

        public CurrentUserService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetGivenName()
        {
            return _context.HttpContext.User.FindFirst(ClaimTypes.GivenName).Value;
        }

        public string GetSurname()
        {
            return _context.HttpContext.User.FindFirst(ClaimTypes.Surname).Value;
        }

        public string GetNameIdentifier()
        {
            if (_context.HttpContext?.User is null)
                return string.Empty;
            return _context.HttpContext?.User?.Claims.FirstOrDefault(c=> c.Type == ClaimConstants.UserId)?.Value;
        }

        public string GetEmails()
        {
            if (_context.HttpContext?.User is null)
                return string.Empty;
            return _context.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimConstants.UserEmail)?.Value;
        }

    }
}
