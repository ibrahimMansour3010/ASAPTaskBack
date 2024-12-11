using ASAPTask.Applications.Common.Interfaces;
using ASAPTask.Applications.Invoice.Queries.InvoiceLookup.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Invoice.Queries.InvoiceLookup
{
    public class InvoiceLookupQueryHandler : IRequestHandler<InvoiceLookupQuery, InvoiceLookupOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepo<ASAPTask.Domain.Entities.Item, long> _itemRepo;

        public InvoiceLookupQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _itemRepo = _unitOfWork.Repository<ASAPTask.Domain.Entities.Item, long>();
        }
        public async Task<InvoiceLookupOutput> Handle(InvoiceLookupQuery request, CancellationToken cancellationToken)
        {
            var result = new InvoiceLookupOutput();
            var items =  _itemRepo.GetNoTrack(filter: c => !c.IsDeleted);

            result.Items = await items.Select(c => new ItemListQueryDto()
            {
                AvailableQuantity = c.AvailableQuantity,
                id = c.Id,
                Name = c.Name,
                Price = 0,
                SelectedQuantity = 0,
                UnitPrice = c.UnitPrice,
            }).ToListAsync();

            return result;
        }
    }
}
