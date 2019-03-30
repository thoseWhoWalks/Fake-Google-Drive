using FGD.Api.Model;
using FluentValidation;

namespace FGD.Api.Validators
{
    public class SharedFileModelApiValidator : AbstractValidator<SharedFileModelApi<int>>
    {
        public SharedFileModelApiValidator()
        {
            RuleFor(o => o.AccountEmail)
                .NotEmpty()
                .EmailAddress();

            RuleFor(o => o.StoredFileId)
                .GreaterThanOrEqualTo(0);
        }
    }
}
