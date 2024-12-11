using ASAPTask.Applications.Invoice.Queries.GetInvoiceForEdit.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Invoice.Queries.GetInvoiceForEdit
{
    public class GetInvoiceForEditQuery:IRequest<GetInvoiceForEditOutput>
    {
        public string InvoiceNumber { get; set; }
    }
}
