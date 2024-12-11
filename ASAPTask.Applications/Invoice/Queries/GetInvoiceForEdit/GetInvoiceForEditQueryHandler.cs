using ASAPTask.Applications.Common.Exceptions;
using ASAPTask.Applications.Common.Interfaces;
using ASAPTask.Applications.Invoice.Queries.GetInvoiceForEdit.Dtos;
using ASAPTask.SharedKernal.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Invoice.Queries.GetInvoiceForEdit
{
    public class GetInvoiceForEditQueryHandler : IRequestHandler<GetInvoiceForEditQuery, GetInvoiceForEditOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepo<ASAPTask.Domain.Entities.Invoice, long> _invoiceRepo;

        public GetInvoiceForEditQueryHandler(IUnitOfWork unitOfWork, CurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _invoiceRepo = _unitOfWork.Repository<ASAPTask.Domain.Entities.Invoice, long>();
        }
        public async Task<GetInvoiceForEditOutput> Handle(GetInvoiceForEditQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepo
                        .GetOneAsyncNoTrack(c=>c.InvoiceNumber == request.InvoiceNumber && !c.IsDeleted,
                        includeProperties: "InvoiceDetails.Item");
            if (invoice == null)
                throw new BusinessException("Invalid Invoice Number");
            var response = new GetInvoiceForEditOutput()
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                TotalPrice = invoice.TotalAmount,
                Items = invoice.InvoiceDetails.Select(c=>new Commands.CreateInvoice.Dtos.ItemListInvoiceDto()
                {
                    AvailableQuantity = c.Item.AvailableQuantity,
                    id = c.ItemId,
                    Name = c.Item.Name,
                    Price = c.TotalItems,
                    UnitPrice = c.UnitPrice,
                    SelectedQuantity = c.Quantity,
                }).ToList()
            };
            return response;
        }
    }
}
