using ASAPTask.Applications.Invoice.Commands.DeleteInvoice.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Invoice.Commands.DeleteInvoice
{
    public class DeleteInvoiceCommand:IRequest<DeleteInvoiceOutput>
    {
        public string InvoiceNumber { get; set; }
    }
}
