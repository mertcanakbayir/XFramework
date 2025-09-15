using FluentValidation;
using XFramework.Dtos.EndpointRole;

namespace XFramework.BLL.Utilities.ValidationRulers
{
    public class EndpointRoleUpdateDtoValidator : AbstractValidator<EndpointRoleUpdateDto>
    {
        public EndpointRoleUpdateDtoValidator()
        {
            RuleFor(x => x.EndpointId).NotEmpty().WithMessage("Endpoint ID is required.");

            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Role ID is required.");
        }
    }
}
