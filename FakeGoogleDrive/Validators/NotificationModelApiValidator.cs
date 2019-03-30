using FGD.Api.Model;
using FluentValidation;

namespace FGD.Api.Validators
{
    public class NotificationModelApiValidator : AbstractValidator<NotificationModelApi<int>>
    {
        public NotificationModelApiValidator()
        {
            RuleFor(o => o.Title)
                .NotEmpty();

            RuleFor(o => o.Id)
                .GreaterThan(-1);

        }
    }
}
