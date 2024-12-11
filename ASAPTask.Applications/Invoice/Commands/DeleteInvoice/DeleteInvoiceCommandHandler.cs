using ASAPTask.Applications.Common.Exceptions;
using ASAPTask.Applications.Common.Interfaces;
using ASAPTask.Applications.Invoice.Commands.DeleteInvoice.Dtos;
using ASAPTask.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Invoice.Commands.DeleteInvoice
{
    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, DeleteInvoiceOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepo<ASAPTask.Domain.Entities.Item, long> _itemRepo;
        private readonly IGenericRepo<ASAPTask.Domain.Entities.Invoice, long> _invoiceRepo;
        private readonly IGenericRepo<ASAPTask.Domain.Entities.InvoiceDetails, long> _invoiceDetailsRepo;

        public DeleteInvoiceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _invoiceRepo = _unitOfWork.Repository<ASAPTask.Domain.Entities.Invoice, long>();
            _invoiceDetailsRepo = _unitOfWork.Repository<ASAPTask.Domain.Entities.InvoiceDetails, long>();
            _itemRepo = _unitOfWork.Repository<ASAPTask.Domain.Entities.Item, long>();
        }
        public async Task<DeleteInvoiceOutput> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepo.GetOneAsync(filter:c=>c.InvoiceNumber == request.InvoiceNumber,
                    includeProperties:$"{nameof(InvoiceDetails)}.{nameof(ASAPTask.Domain.Entities.Item)}");
            if (invoice == null)
                throw new BusinessException("Invalid Invoice Number");
            invoice.IsDeleted = true;
            invoice.InvoiceDetails.ToList().ForEach(c =>
            {
                c.Item.AvailableQuantity += c.Quantity;
            });
            _invoiceRepo.Update(invoice);
            await _unitOfWork.CompleteAsync();
            return new DeleteInvoiceOutput()
            {
                InvoiceNumber = invoice.InvoiceNumber,
            };
        }
    }
}
