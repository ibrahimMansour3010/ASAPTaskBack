using ASAPTask.Applications.Item.Commands.UpdateItem.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Item.Commands.UpdateItem
{
    public class UpdateItemCommand:IRequest<UpdateItemOutput>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public long AvailableQuantity { get; set; }
    }
}
