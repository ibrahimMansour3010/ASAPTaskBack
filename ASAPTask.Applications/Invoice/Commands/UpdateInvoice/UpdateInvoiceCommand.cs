using ASAPTask.Applications.Invoice.Commands.CreateInvoice.Dtos;
using ASAPTask.Applications.Invoice.Commands.UpdateInvoice.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Invoice.Commands.UpdateInvoice
{
    public class UpdateInvoiceCommand:IRequest<UpdateInvoiceOutput>
    {
        public long Id { get; set; }
        public List<ItemListInvoiceDto> Items { get; set; } = new List<ItemListInvoiceDto>();
        public double TotalPrice { get; set; }
    }
}
