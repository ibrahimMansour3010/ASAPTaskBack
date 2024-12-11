using ASAPTask.Applications.Common.Dtos;
using ASAPTask.Applications.Invoice.Queries.InvoiceList.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Invoice.Queries.InvoiceList
{
    public class InvoiceListQuery : BasePagenationDto, IRequest<InvoiceListOutput>
    {
    }
}
