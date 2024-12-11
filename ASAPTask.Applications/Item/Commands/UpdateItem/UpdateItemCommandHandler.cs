using ASAPTask.Applications.Common.Exceptions;
using ASAPTask.Applications.Common.Interfaces;
using ASAPTask.Applications.Item.Commands.UpdateItem.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Item.Commands.UpdateItem
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, UpdateItemOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepo<ASAPTask.Domain.Entities.Item, long> _itemRepo;

        public UpdateItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _itemRepo = _unitOfWork.Repository<ASAPTask.Domain.Entities.Item, long>();
        }
        public async Task<UpdateItemOutput> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _itemRepo.GetOneAsyncNoTrack(c=>c.Id== request.Id);
            if (item == null)
                throw new BusinessException("Item Not Founded");
            item.Name = request.Name;
            item.Description = request.Description;
            item.UnitPrice = request.UnitPrice;
            item.AvailableQuantity = request.AvailableQuantity;
            _itemRepo.Update(item);
            await _unitOfWork.CompleteAsync();
            return new UpdateItemOutput()
            {
                AvailableQuantity = item.AvailableQuantity,
                UnitPrice = item.UnitPrice,
                Description = item.Description,
                Id = item.Id,
                Name = item.Name,
            };
        }
    }
}
