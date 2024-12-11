using ASAPTask.Applications.Invoice.Commands.CreateInvoice.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Invoice.Queries.GetInvoiceForEdit.Dtos
{
    public class GetInvoiceForEditOutput
    {
        public long Id { get; set; }
        public string InvoiceNumber { get; set; }
        public List<ItemListInvoiceDto> Items { get; set; } = new List<ItemListInvoiceDto>();
        public double TotalPrice { get; set; }
    }
}
