using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Application.Commands;
using FluentValidation;

namespace TodoApi.Application.Validators
{
    public class CommandUpdateValidator : AbstractValidator<CommandUpdate>
    {
        public CommandUpdateValidator() 
        {
            RuleFor(command => command.Id).GreaterThan(0).WithMessage("ID must be greater than 0");
            RuleFor(command => command.Name).NotEmpty().WithMessage("Name cannot be empty.");
            
        }
    }
}
