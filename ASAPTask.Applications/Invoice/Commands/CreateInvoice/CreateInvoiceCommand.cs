using ASAPTask.Applications.Invoice.Commands.CreateInvoice.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Invoice.Commands.CreateInvoice
{
    public class CreateInvoiceCommand:IRequest<CreateInvoiceOutput>
    {
        public List<ItemListInvoiceDto> Items { get; set; } = new List<ItemListInvoiceDto>();
        public double TotalPrice { get; set; }
    }
}
