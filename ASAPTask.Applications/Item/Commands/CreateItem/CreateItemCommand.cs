using ASAPTask.Applications.Item.Commands.CreateItem.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Item.Commands.CreateItem
{
    public class CreateItemCommand:IRequest<CreateItemOutput>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
