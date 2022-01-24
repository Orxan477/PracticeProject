using FluentValidation;
using PracticeProject.ViewModels.Account;

namespace PracticeProject.Validators.AccountValidator
{
    public class RegisterValidator:AbstractValidator<RegisterVM>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FullName).NotEmpty()
                                  .NotNull()
                                  .MaximumLength(100);

            RuleFor(x => x.UserName).NotEmpty()
                                    .NotNull()
                                    .MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty()
                                 .NotNull()
                                 .EmailAddress();
            RuleFor(x => x.Password).NotNull()
                                  .NotEmpty();
            RuleFor(x => x.RePassword).Equal(x => x.Password);
                                  
        }
    }
}
