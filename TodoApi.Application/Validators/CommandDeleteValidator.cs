using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TodoApi.Application.Commands;

namespace TodoApi.Application.Validators
{
    internal class CommandDeleteValidator : AbstractValidator<CommandDelete>
    {
        public CommandDeleteValidator() 
        {
            RuleFor(command => command.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
