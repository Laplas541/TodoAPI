using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using TodoApi.Application.Commands;
using TodoApi.Infrastructure.Context;
using FluentValidation;

namespace TodoApi.Application.Handlers
{
    public class CommandDeleteHandler : ICommandHandler<CommandDelete>
    {
        private readonly TodoContext _context;
        private readonly IValidator<CommandDelete> _deletevalidator;
        public CommandDeleteHandler(TodoContext context, IValidator<CommandDelete> validator) {
            _context = context;
            _deletevalidator = validator;
        }

        public async Task HandleAsync(CommandDelete command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _deletevalidator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

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
