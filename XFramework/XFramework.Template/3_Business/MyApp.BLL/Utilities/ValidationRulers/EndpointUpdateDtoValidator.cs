using FluentValidation;
using MyApp.Dtos.Endpoint;

namespace MyApp.BLL.Utilities.ValidationRulers
{
    public class EndpointUpdateDtoValidator : AbstractValidator<EndpointUpdateDto>
    {
        public EndpointUpdateDtoValidator()
        {
            RuleFor(e => e.HttpMethod).NotEmpty().WithMessage("Http method is required.");
        }
    }
}
