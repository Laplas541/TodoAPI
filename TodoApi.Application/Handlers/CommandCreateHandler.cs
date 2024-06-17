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



namespace TodoApi.Application.Handlers
{
    public class CommandCreateHandler : ICommandHandler<CommandCreate>
    {
        
        private readonly TodoContext _context;
        private readonly IMapper _mapper;
        public CommandCreateHandler(TodoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task HandleAsync(CommandCreate command, CancellationToken cancellationToken = default)
        {
            var todomapper = _mapper.Map<TodoItem>(command);
            _context.TodoItems.Add(todomapper);
            await _context.SaveChangesAsync();

            
        }
    }
}
