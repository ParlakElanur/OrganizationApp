using FluentValidation;
using Organization_MVC.ViewModels;

namespace Organization_MVC.Validator
{
    public class UserDetailViewModelValidator : AbstractValidator<UserDetailViewModel>
    {
        public UserDetailViewModelValidator()
        {
            RuleFor(u => u.Name).NotNull().WithMessage("İsim boş geçilemez")
                                          .MaximumLength(30).WithMessage("En fazla 30 karakter olmalıdır");

            RuleFor(u => u.Surname).NotEmpty().WithMessage("Soyisim boş geçilemez")
                                   .MaximumLength(30).WithMessage("En fazla 30 karakter olmalıdır");

            RuleFor(u => u.Email).EmailAddress().WithMessage("Mail formatında olmalıdır")
                                 .NotNull().WithMessage("Email boş geçilemez")
                                 .MaximumLength(30).WithMessage("En fazla 30 karakter olmalıdır");

            RuleFor(u => u.Password).NotNull().WithMessage("Şifre boş geçilemez")
                                    .Matches("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,30})$").WithMessage("Şifreniz en az 8 karakter ve en fazla 30 karakter olmalı, harf ve rakam içermelidir.");

            RuleFor(u => u.RePassword).NotEmpty().WithMessage("Şifre doğrulama boş geçilemez")
                                      .Equal(u => u.Password).WithMessage("Şifre tekrarı hatalı");
        }
    }
}
