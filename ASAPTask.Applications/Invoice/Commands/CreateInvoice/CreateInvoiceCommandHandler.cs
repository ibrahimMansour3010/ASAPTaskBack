using ASAPTask.Applications.Common.Exceptions;
using ASAPTask.Applications.Common.Interfaces;
using ASAPTask.Applications.Invoice.Commands.CreateInvoice.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Invoice.Commands.CreateInvoice
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, CreateInvoiceOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepo<ASAPTask.Domain.Entities.Item, long> _itemRepo;
        private readonly IGenericRepo<ASAPTask.Domain.Entities.Invoice, long> _invoiceRepo;
        private readonly IGenericRepo<ASAPTask.Domain.Entities.InvoiceDetails, long> _invoiceDetailsRepo;

        public CreateInvoiceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _invoiceRepo = _unitOfWork.Repository<ASAPTask.Domain.Entities.Invoice, long>();
            _invoiceDetailsRepo = _unitOfWork.Repository<ASAPTask.Domain.Entities.InvoiceDetails, long>();
            _itemRepo = _unitOfWork.Repository<ASAPTask.Domain.Entities.Item, long>();
        }
        public async Task<CreateInvoiceOutput> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = new ASAPTask.Domain.Entities.Invoice()
            {
                TotalAmount = request.TotalPrice,
            };

            foreach (var item in request.Items)
            {
                var itemEntity = await _itemRepo.GetOneAsync(filter: c => c.Id == item.id);
                if (item.SelectedQuantity > itemEntity.AvailableQuantity)
                    throw new BusinessException("Not Avaiable Amount");
                itemEntity.AvailableQuantity -= item.SelectedQuantity;
                _itemRepo.Update(itemEntity);
                var details = new Domain.Entities.InvoiceDetails()
                {
                    UnitPrice = item.UnitPrice,
                    ItemId = item.id,
                    TotalItems = item.Price,
                    Quantity = item.SelectedQuantity,
                    Invoice = invoice
                };
                await _invoiceDetailsRepo.InsertAsync(details);
            }
            await _unitOfWork.CompleteAsync();
            return new CreateInvoiceOutput()
            {
                InvoiceId = invoice.Id,
            };
        }
    }
}
