using FluentValidation;
using XFramework.Dtos.Endpoint;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class EndpointAddDtoValidator : AbstractValidator<EndpointAddDto>
    {
        public EndpointAddDtoValidator()
        {
            RuleFor(e => e.HttpMethod).NotEmpty().WithMessage("Http method is required.");
            RuleFor(e => e.Controller).NotEmpty().WithMessage("Controller name is required.");
            RuleFor(e => e.Action).NotEmpty().WithMessage("Action name is required.");
        }
    }
}
