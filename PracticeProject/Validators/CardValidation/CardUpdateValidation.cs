using FluentValidation;
using PracticeProject.ViewModels.Card;

namespace PracticeProject.Validators.CardValidation
{
    public class CardUpdateValidation:AbstractValidator<CardUpdateVM>
    {
        public CardUpdateValidation()
        {
            RuleFor(x => x.Name).NotNull()
                              .NotEmpty()
                              .MaximumLength(50);
            RuleFor(x => x.Content).NotNull()
                                 .NotEmpty()
                                 .MaximumLength(255);
        }
    }
}
