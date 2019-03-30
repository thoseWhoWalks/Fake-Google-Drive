using FGD.Api.Model;
using FluentValidation;

namespace FGD.Api.Validators
{
    public class LoginModelApiValidator : AbstractValidator<LoginModelApi>
    {
        public LoginModelApiValidator()
        {
            RuleFor(o => o.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(o => o.Password)
                .NotEmpty()
                .MinimumLength(6);
                
        }
    }
}
