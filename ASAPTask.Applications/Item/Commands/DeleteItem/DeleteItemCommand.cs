using ASAPTask.Applications.Item.Commands.DeleteItem.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Item.Commands.DeleteItem
{
    public class DeleteItemCommand:IRequest<DeleteItemOutput>
    {
        public long Id { get; set; }
    }
}
