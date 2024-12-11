using ASAPTask.Applications.Item.Commands.UpdateItem;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Item.Commands.DeleteItem
{
    public class DeleteItemCommandValidator: AbstractValidator<DeleteItemCommand>
    {
        public DeleteItemCommandValidator()
        {
            RuleFor(c=>c.Id).NotNull().NotEmpty().NotEqual(0);
        }
    }
}
