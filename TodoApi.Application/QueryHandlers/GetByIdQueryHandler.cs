using System;
using System.Threading;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using TodoApi.Application.Convey_Queries;
using TodoApi.Domain.Models;
using TodoApi.Infrastructure.Context;

public class GetByIdQueryHandler : IQueryHandler<GetByIdQuery, TodoItem>
{
    private readonly TodoContext _context;

    public GetByIdQueryHandler(TodoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<TodoItem> HandleAsync(GetByIdQuery query, CancellationToken cancellationToken = default)
    {
        var todoItem = await _context.TodoItems.FindAsync(new object[] { query.Id }, cancellationToken);

        if (todoItem == null)
        {
            throw new KeyNotFoundException($"TodoItem with ID {query.Id} not found.");
            
        }

        return todoItem;
    }
}
