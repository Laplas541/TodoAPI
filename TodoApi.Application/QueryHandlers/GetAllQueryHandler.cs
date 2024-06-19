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
using FluentValidation;

namespace TodoApi.Application.QueryHandlers
{
    public class GetAllQueryHandler : IQueryHandler<GetAllQuery, IEnumerable<TodoItem>>
    {
        private readonly TodoContext _context;
        private readonly IValidator<GetAllQuery> _validator;
        public GetAllQueryHandler(TodoContext context, IValidator<GetAllQuery> validator) 
        { 
            _context = context;
            _validator = validator;
        }

        public async Task<IEnumerable<TodoItem>> HandleAsync(GetAllQuery query, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            return await _context.TodoItems.ToListAsync(); 
        }
    }
}
