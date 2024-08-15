using FluentValidation;
using ShopApp.Apps.AdminApp.Dtos.UserDto;

namespace ShopApp.Apps.AdminApp.Validators.UserValidators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(c => c.UserName)
                .NotNull()
                .MaximumLength(30);

            RuleFor(c => c.Password)
                .NotNull()
                .MaximumLength(10)
                .MinimumLength(6);
        }
    }
}
