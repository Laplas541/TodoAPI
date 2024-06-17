using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TodoApi.Application.Convey_Queries;

namespace TodoApi.Application.Validators
{
    public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
    {
        public GetByIdQueryValidator() 
        {
            RuleFor(query => query.Id)
                .GreaterThan(0).WithMessage("ID must be greater than 0");
        }
    }
}
