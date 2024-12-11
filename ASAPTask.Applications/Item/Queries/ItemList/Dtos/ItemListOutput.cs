using ASAPTask.Applications.Common.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Item.Queries.ItemList.Dtos
{
    public class ItemListOutput
    {
        public int AllItemCount { get; set; }
        public IList<ItemListDto> Result { get; set; }
    }
}
