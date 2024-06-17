using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Microsoft.EntityFrameworkCore;
using TodoApi.Application.Convey_Queries;
using TodoApi.Domain.Models;
using TodoApi.Infrastructure.Context;

namespace TodoApi.Application.QueryHandlers
{
    public class GetAllQueryHandler : IQueryHandler<GetAllQuery, IEnumerable<TodoItem>>
    {
        private readonly TodoContext _context;
        public GetAllQueryHandler(TodoContext context) 
        { 
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> HandleAsync(GetAllQuery query, CancellationToken cancellationToken = default)
        {
            return await _context.TodoItems.ToListAsync(); 
        }
    }
}
