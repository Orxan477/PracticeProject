using FluentValidation;
using PracticeProject.ViewModels.Settings;

namespace PracticeProject.Validators.Setting
{
    public class SettingUpdateValidator:AbstractValidator<SettingUpdateVM>
    {
        public SettingUpdateValidator()
        {
            RuleFor(x => x.Value).NotNull().NotEmpty();
        }
    }
}
