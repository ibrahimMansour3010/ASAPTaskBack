using ASAPTask.Applications.Common.Interfaces;
using ASAPTask.Applications.Item.Queries.ItemList.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Item.Queries.ItemList
{
    public class ItemListQueryHandler : IRequestHandler<ItemListQuery, ItemListOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepo<ASAPTask.Domain.Entities.Item, long> _itemRepo;

        public ItemListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _itemRepo = _unitOfWork.Repository<ASAPTask.Domain.Entities.Item, long>();
        }
        public async Task<ItemListOutput> Handle(ItemListQuery request, CancellationToken cancellationToken)
        {
            var itmes = await _itemRepo.GetPaginatedAsync(filter: c => !c.IsDeleted, pageSize: request.PageSize,
                page: request.PageNumber,orderBy:c=>c.OrderByDescending(z=>z.CreatedAt));
            var output = new ItemListOutput();
            output.AllItemCount = await _itemRepo.CountAsync(filter: c => !c.IsDeleted);
            output.Result = itmes.Select(c=>new ItemListDto()
            {
                Id = c.Id,
                AvailableQuantity = c.AvailableQuantity,
                Description = c.Description,
                Name = c.Name,
                UnitPrice = c.UnitPrice
            }).ToList();

            return output;
        }
    }
}
