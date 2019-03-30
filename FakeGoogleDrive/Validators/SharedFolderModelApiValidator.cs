using FGD.Api.Model;
using FluentValidation;

namespace FGD.Api.Validators
{
    public class SharedFolderModelApiValidator : AbstractValidator<SharedFolderModelApi<int>>
    {
        public SharedFolderModelApiValidator()
        {
            RuleFor(o => o.AccountEmail)
                .NotEmpty()
                .EmailAddress();

            RuleFor(o => o.StoredFolderId)
                .GreaterThanOrEqualTo(0);
        }
    }
}
