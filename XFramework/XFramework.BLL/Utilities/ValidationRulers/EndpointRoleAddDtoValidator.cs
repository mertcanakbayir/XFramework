using FluentValidation;
using XFramework.Dtos.EndpointRole;

namespace XFramework.BLL.Utilities.ValidationRulers
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
