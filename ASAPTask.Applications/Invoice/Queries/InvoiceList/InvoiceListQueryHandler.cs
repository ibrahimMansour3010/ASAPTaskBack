using ASAPTask.Applications.Common.Interfaces;
using ASAPTask.Applications.Invoice.Queries.InvoiceList.Dtos;
using ASAPTask.SharedKernal.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Invoice.Queries.InvoiceList
{
    public class InvoiceListQueryHandler : IRequestHandler<InvoiceListQuery, InvoiceListOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepo<ASAPTask.Domain.Entities.Invoice, long> _invoiceRepo;
        private readonly CurrentUserService _currentUserService;

        public InvoiceListQueryHandler(IUnitOfWork unitOfWork, CurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _invoiceRepo = _unitOfWork.Repository<ASAPTask.Domain.Entities.Invoice, long>();
            _currentUserService = currentUserService;
        }
        public async Task<InvoiceListOutput> Handle(InvoiceListQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetNameIdentifier();
            var invoices = await _invoiceRepo.GetPaginatedAsync(filter: c => !c.IsDeleted && c.CreatedBy == currentUserId,
                pageSize:request.PageSize,page:request.PageNumber);
            var invoicesCount = await _invoiceRepo.CountAsync(filter: c => !c.IsDeleted && c.CreatedBy == currentUserId);
            InvoiceListOutput result = new InvoiceListOutput();
            result.AllItemCount = invoicesCount;
            result.Result = await invoices.Select(c => new InvoiceListDto()
            {
                CreationDate = c.CreatedAt.Value.ToString("dd-MM-yyyy"),
                TotalPrice = c.TotalAmount,
                InvoiceNumber = c.InvoiceNumber,
            }).ToListAsync();
            return result;
        }
    }
}
