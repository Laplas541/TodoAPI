using Convey;
using Convey.CQRS.Queries;
using Convey.CQRS.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Application.Convey_Queries;
using TodoApi.Application.QueryHandlers;
using TodoApi.Domain.Models;

namespace TodoApi.Application
{
    public static class ConveyExtensions
    {
        public static IServiceCollection AddConveyServices(this IServiceCollection services)
        {
            var builder = services.AddConvey()
                .AddCommandHandlers()
                .AddInMemoryCommandDispatcher()
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher();

            services.AddScoped<IQueryHandler<GetAllQuery, IEnumerable<TodoItem>>, GetAllQueryHandler>();

            return services;
        }
    }
}

