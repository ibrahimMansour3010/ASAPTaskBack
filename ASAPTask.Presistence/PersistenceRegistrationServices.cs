using ASAPTask.Applications.Common.Interfaces;
using ASAPTask.Presistence.Repositories.GenericRepo;
using ASAPTask.Presistence.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Presistence
{
    public static class PersistenceRegistrationServices
    {
        public static IServiceCollection RegisterPersistence(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWorkImplementaion>();
            services.AddScoped(typeof(IGenericRepo<,>), typeof(GenericRepo<,>));

            return services;
        }
    }
}
