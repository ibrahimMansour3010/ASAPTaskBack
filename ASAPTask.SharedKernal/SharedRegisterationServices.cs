using ASAPTask.SharedKernal.Helpers;
using ASAPTask.SharedKernal.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.SharedKernal
{
    public static class SharedRegisterationServices
    {
        public static IServiceCollection RegisterSharedKernel(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<CurrentUserService>();

            return services;
        }
    }
}
