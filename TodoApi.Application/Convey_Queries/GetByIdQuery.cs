using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using TodoApi.Domain.Models;

namespace TodoApi.Application.Convey_Queries
{
    public class GetByIdQuery : IQuery<TodoItem>
    {
        public long Id { get; set; } 
        public GetByIdQuery(long id)
        {
            Id = id;
        }

    }
}
