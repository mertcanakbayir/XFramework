using FluentValidation;
using XFramework.Dtos.Endpoint;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class EndpointUpdateDtoValidator : AbstractValidator<EndpontUpdateDto>
    {
        public EndpointUpdateDtoValidator()
        {
            RuleFor(e => e.HttpMethod).NotEmpty().WithMessage("Http method can not be null");
        }
    }
}
