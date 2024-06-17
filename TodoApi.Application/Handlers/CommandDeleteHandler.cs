using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using TodoApi.Application.Commands;
using TodoApi.Infrastructure.Context;

namespace TodoApi.Application.Handlers
{
    public class CommandDeleteHandler : ICommandHandler<CommandDelete>
    {
        private readonly TodoContext _context;
        public CommandDeleteHandler(TodoContext context) {
            _context = context;
        }

        public async Task HandleAsync(CommandDelete command, CancellationToken cancellationToken = default)
        {
            var todoItem = await _context.TodoItems.FindAsync(command.Id);
            if (todoItem == null)
            {
                throw new KeyNotFoundException($"Todo Item With ID {command.Id} Not Found");
            }
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }
    }
}
