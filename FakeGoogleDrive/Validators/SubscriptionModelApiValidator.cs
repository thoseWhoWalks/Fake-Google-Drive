using FGD.Api.Model;
using FluentValidation;

namespace FGD.Api.Validators
{
    public class SubscriptionModelApiValidator : AbstractValidator<SubscriptionModelApi<int>>
    {
        public SubscriptionModelApiValidator()
        {
            RuleFor(o => o.Title)
               .NotEmpty();

            RuleFor(o => o.TotalSpace)
                .GreaterThan(0);

            RuleFor(o => o.Id)
                .GreaterThan(-1);
        }
    }
}
