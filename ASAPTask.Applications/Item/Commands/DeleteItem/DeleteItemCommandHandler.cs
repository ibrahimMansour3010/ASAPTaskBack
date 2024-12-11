using ASAPTask.Applications.Common.Exceptions;
using ASAPTask.Applications.Common.Interfaces;
using ASAPTask.Applications.Item.Commands.DeleteItem.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Item.Commands.DeleteItem
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, DeleteItemOutput>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepo<ASAPTask.Domain.Entities.Item, long> _itemRepo;

        public DeleteItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _itemRepo = _unitOfWork.Repository<ASAPTask.Domain.Entities.Item, long>();
        }
        public async Task<DeleteItemOutput> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _itemRepo.GetOneAsyncNoTrack(c => c.Id == request.Id);
            if (item == null)
                throw new BusinessException("Item Not Founded");
            item.IsDeleted = true;
            _itemRepo.Update(item);
            await _unitOfWork.CompleteAsync();
            return new DeleteItemOutput() { ItemId = item.Id };
        }
    }
}
