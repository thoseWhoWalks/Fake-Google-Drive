using FGD.Api.Model;
using FluentValidation;

namespace FGD.Api.Validators
{
    public class AccountModelApiValidator : AbstractValidator<AccountModelApi<int>>
    {
        public AccountModelApiValidator()
        {
            RuleFor(o => o.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(o => o.Password)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(o => o.IsDelted)
                .Equals(false);

            RuleFor(o => o.Age)
                .InclusiveBetween(1, 300);
        }
    }
}
