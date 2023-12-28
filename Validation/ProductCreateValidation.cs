using FluentValidation;
using WEB_API.Models.DTO;

namespace WEB_API.Validation
{
    public class ProductCreateValidation : AbstractValidator<ProductCreateDTO>
    {
        public ProductCreateValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
