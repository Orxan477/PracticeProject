using FluentValidation;
using PracticeProject.ViewModels.Card;

namespace PracticeProject.Validators.CardValidation
{
    public class CardCreateValidation:AbstractValidator<CardCreateVM>
    {
        public CardCreateValidation()
        {
            RuleFor(x => x.Name).NotNull()
                              .NotEmpty()
                              .MaximumLength(50);
            RuleFor(x => x.Photo).NotNull();
            RuleFor(x => x.Content).NotNull()
                                 .NotEmpty()
                                 .MaximumLength(255);
        }
    }
}
