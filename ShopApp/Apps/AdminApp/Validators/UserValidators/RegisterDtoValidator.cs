using FluentValidation;
using ShopApp.Apps.AdminApp.Dtos.UserDto;

namespace ShopApp.Apps.AdminApp.Validators.UserValidators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(r => r.FullName)
                .NotNull()
                .WithMessage("Full Name is required..")
                .MaximumLength(30)
                .WithMessage("Full Name cannot exceed 30 characters..");

            RuleFor(r => r.UserName)
                .NotNull()
                .WithMessage("Username is required..")
                .MaximumLength(30)
                .WithMessage("Username cannot exceed 30 characters..");

            RuleFor(r => r.Email)
                .NotNull().WithMessage("Email is required..")
                .EmailAddress().WithMessage("A valid email address is required..")
                .MaximumLength(50).WithMessage("Email cannot exceed 50 characters..");


            RuleFor(r => r.Password)
                .MaximumLength(10)
                .WithMessage("Password cannot exceed 10 characters..")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long..");

            RuleFor(r => r.RePassword)
                .MaximumLength(15)
                .WithMessage("Re-entered Password cannot exceed 15 characters..")
                .MinimumLength(6)
                .WithMessage("Re-entered Password must be at least 6 characters long..")
                .Equal(r => r.Password)
                .WithMessage("Password and Re-entered Password do not match..");
        }
    }
}
