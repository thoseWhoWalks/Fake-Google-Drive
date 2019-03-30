using FGD.Api.Model;
using FluentValidation;

namespace FGD.Api.Validators
{
    public class AccountSubscriptionModelApiValidator : AbstractValidator<AccountSubscriptionModelApi<int>>
    {
        public AccountSubscriptionModelApiValidator()
        {
            RuleFor(o => o.SubscriptionId)
                .NotEmpty();
        }
    }
}
