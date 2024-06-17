using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TodoApi.Application.Convey_Queries;

namespace TodoApi.Application.Validators
{
    public class GetAllQueryValidator : AbstractValidator<GetAllQuery>
    {
        public GetAllQueryValidator() 
        {
            RuleFor(query => query.Pagenumber)
                .GreaterThanOrEqualTo(1).WithMessage("Page number must be at least 1");
                
        }
    }
}
