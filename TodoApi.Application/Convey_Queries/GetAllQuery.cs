using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using TodoApi.Domain.Models;

namespace TodoApi.Application.Convey_Queries
{
    public class GetAllQuery : IQuery<IEnumerable<TodoItem>>
    {
        public int Pagenumber { get; set; }
    }
}
