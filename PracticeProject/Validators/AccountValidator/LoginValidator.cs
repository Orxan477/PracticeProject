using FluentValidation;
using PracticeProject.ViewModels.Account;

namespace PracticeProject.Validators.AccountValidator
{
    public class LoginValidator:AbstractValidator<LoginVM>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                                .NotNull()
                                .EmailAddress();
            RuleFor(x => x.Password).NotNull()
                                  .NotEmpty();
        }
    }
}
