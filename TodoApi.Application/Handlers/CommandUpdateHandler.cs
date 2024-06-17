using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Convey.CQRS.Commands;
using Microsoft.EntityFrameworkCore;
using TodoApi.Application.Commands;
using TodoApi.Domain.Models;
using TodoApi.Infrastructure.Context;

namespace TodoApi.Application.Handlers
{
     public class CommandUpdateHandler : ICommandHandler<CommandUpdate>
    {
        private readonly TodoContext _context;
        private readonly IMapper _mapper;

        public CommandUpdateHandler(TodoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task HandleAsync(CommandUpdate command, CancellationToken cancellationToken = default)
        {
            var todoItem = await _context.TodoItems.FindAsync(command.Id);
            if (todoItem == null)
            {
                
                throw new KeyNotFoundException($"Todo item with ID {command.Id} not found.");
            }

            _mapper.Map(command, todoItem);

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                
                throw new DbUpdateConcurrencyException($"Concurrency issue when updating Todo item with ID {command.Id}");
            }
        }
    }

}