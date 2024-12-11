using ASAPTask.Applications.Common.Handlers;
using ASAPTask.Applications.Common.Interfaces;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Common
{
    public static class ApplicationRegisteration
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserHandler, UserHandler>();
            services.AddFluentValidation(r => r.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddFluentValidationAutoValidation();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
