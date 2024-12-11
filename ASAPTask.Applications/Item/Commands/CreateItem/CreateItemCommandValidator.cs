using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Item.Commands.CreateItem
{
    public class CreateItemCommandValidator:AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator()
        {
            RuleFor(c=>c.UnitPrice).NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(c=>c.AvailableQuantity).NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(c=>c.Name).NotNull().NotEmpty();
            RuleFor(c=>c.Description).NotNull().NotEmpty();
        }
    }
}
