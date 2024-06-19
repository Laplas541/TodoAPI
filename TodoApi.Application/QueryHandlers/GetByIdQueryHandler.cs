using System;
using System.Threading;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using TodoApi.Application.Convey_Queries;
using TodoApi.Domain.Models;
using TodoApi.Infrastructure.Context;
using FluentValidation;

public class GetByIdQueryHandler : IQueryHandler<GetByIdQuery, TodoItem>
{
    private readonly TodoContext _context;
    private readonly IValidator<GetByIdQuery> _idvalidator;

    public GetByIdQueryHandler(TodoContext context, IValidator<GetByIdQuery> idvalidator)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _idvalidator = idvalidator;
    }

    public async Task<TodoItem> HandleAsync(GetByIdQuery query, CancellationToken cancellationToken = default)
    {
        var validationResult = await _idvalidator.ValidateAsync(query);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var todoItem = await _context.TodoItems.FindAsync(new object[] { query.Id }, cancellationToken);

        if (todoItem == null)
        {
            throw new KeyNotFoundException($"TodoItem with ID {query.Id} not found.");
            
        }

        return todoItem;
    }
}
