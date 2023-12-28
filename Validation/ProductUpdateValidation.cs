using FluentValidation;
using WEB_API.Models.DTO;

namespace WEB_API.Validation
{
    public class ProductUpdateValidation : AbstractValidator<ProductUpdateDTO>
    {
        public ProductUpdateValidation()
        {
            RuleFor(i => i.Id).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
