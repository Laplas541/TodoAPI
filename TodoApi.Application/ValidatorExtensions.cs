using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Application.Commands;
using TodoApi.Application.Convey_Queries;
using TodoApi.Application.Validators;

namespace TodoApi.Application
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<GetAllQuery>, GetAllQueryValidator>();
            services.AddTransient<IValidator<GetByIdQuery>, GetByIdQueryValidator>();
            services.AddTransient<IValidator<CommandUpdate>, CommandUpdateValidator>();
            services.AddTransient<IValidator<CommandCreate>, CommandCreateValidator>();
            services.AddTransient<IValidator<CommandDelete>, CommandDeleteValidator>();

            return services;
        }
    }
}
