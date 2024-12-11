using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Invoice.Commands.CreateInvoice.Dtos
{
    public class ItemListInvoiceDto
    {
        public long id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public long AvailableQuantity { get; set; }
        public long SelectedQuantity { get; set; }
        public double Price { get; set; }
    }
}
