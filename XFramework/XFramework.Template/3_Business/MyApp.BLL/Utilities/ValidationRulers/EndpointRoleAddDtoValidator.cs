using FluentValidation;
using MyApp.Dtos.EndpointRole;

namespace MyApp.BLL.Utilities.ValidationRulers
{
    public class EndpointRoleAddDtoValidator : AbstractValidator<EndpointRoleAddDto>
    {
        public EndpointRoleAddDtoValidator()
        {
            RuleFor(x => x.EndpointId).NotEmpty().WithMessage("Endpoint ID is required.");

            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Role ID is required.");
        }
    }
}
