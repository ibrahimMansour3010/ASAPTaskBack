using ASAPTask.Applications.Common.Interfaces;
using ASAPTask.Applications.Item.Commands.CreateItem.Dtos;
using ASAPTask.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Item.Commands.CreateItem
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, CreateItemOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepo<ASAPTask.Domain.Entities.Item,long> _itemRepo;

        public CreateItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _itemRepo = _unitOfWork.Repository<ASAPTask.Domain.Entities.Item, long>();
        }
        public async Task<CreateItemOutput> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var itemEntity = await _itemRepo.InsertAsync(new Domain.Entities.Item()
            {
                Name = request.Name,
                UnitPrice = request.UnitPrice,
                Description = request.Description,
                AvailableQuantity = request.AvailableQuantity,
            });
            await _unitOfWork.CompleteAsync();
            return new CreateItemOutput() { ItemId = itemEntity.Id};
        }
    }
}
