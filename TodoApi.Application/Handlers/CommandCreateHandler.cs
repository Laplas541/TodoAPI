using Convey.CQRS.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Application.Commands;
using TodoApi.Domain.Models;
using TodoApi.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FluentValidation;
using TodoApi.Application.Validators;



namespace TodoApi.Application.Handlers
{
    public class CommandCreateHandler : ICommandHandler<CommandCreate>
    {
        
        private readonly TodoContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CommandCreate> _createvalidator;
        public CommandCreateHandler(TodoContext context, IMapper mapper, IValidator<CommandCreate> validator)
        {
            _context = context;
            _mapper = mapper;
            _createvalidator = validator;
          
        }

        public async Task HandleAsync(CommandCreate command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _createvalidator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var todomapper = _mapper.Map<TodoItem>(command);
            _context.TodoItems.Add(todomapper);
            await _context.SaveChangesAsync();

            
        }
    }
}
