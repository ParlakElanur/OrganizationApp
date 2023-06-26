using FluentValidation;
using Organization_MVC.ViewModels;

namespace Organization_MVC.Validator
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(u => u.Email).EmailAddress().WithMessage("Mail formatında olmalıdır")
                                 .NotNull().WithMessage("Email boş geçilemez")
                                 .MaximumLength(30).WithMessage("En fazla 30 karakter olmalıdır");

            RuleFor(u => u.Password).NotNull().WithMessage("Şifre boş geçilemez")
                                    .Matches("^(.{0,7}|[^0-9]*|[^A-Z]*|[a-zA-Z0-9]*)$").WithMessage("Hatalı şifre")
                                    .MaximumLength(30).WithMessage("En fazla 30 karakter olmalıdır");

            RuleFor(u => u.Role).MaximumLength(10).WithMessage("En fazla 10 karakter olmalıdır");
        }
    }
}
