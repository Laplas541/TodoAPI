using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace TodoApi.Application.Commands
{
    public class CommandDelete : ICommand
    {
        public long Id { get; set; }
        public CommandDelete(long id)
        {
            Id = id;
        }

    }
}
