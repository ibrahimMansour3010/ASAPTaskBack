using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Item.Queries.ItemList.Dtos
{
    public class ItemListDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public long AvailableQuantity { get; set; }
    }
}
