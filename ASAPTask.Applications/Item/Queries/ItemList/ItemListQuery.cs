using ASAPTask.Applications.Common.Dtos;
using ASAPTask.Applications.Item.Queries.ItemList.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Item.Queries.ItemList
{
    public class ItemListQuery: BasePagenationDto, IRequest<ItemListOutput>
    {
    }
}
