using FluentValidation;
using Organization_MVC.ViewModels;

namespace Organization_MVC.Validator
{
    public class ActivityViewModelValidator : AbstractValidator<ActivityViewModel>
    {
        public ActivityViewModelValidator()
        {
            RuleFor(a => a.Name).NotNull().WithMessage("Etkinlik ismi boş geçilemez")
                                .MaximumLength(50).WithMessage("Maksimum 50 karakter olmalıdır");

            RuleFor(a => a.Detail).NotNull().WithMessage("Etkinlik detayı boş geçilemez")
                                  .MaximumLength(600).WithMessage("Maksimum 600 karakter olmalıdır");

            RuleFor(a => a.CategoryName).NotNull().WithMessage("Kategori boş geçilemez")
                                        .MaximumLength(40).WithMessage("Maksimum 40 karakter olmalıdır");

            RuleFor(a => a.Date).NotNull().WithMessage("Etkinlik tarihi seçiniz");

            RuleFor(a => a.ActivityDeadline).NotNull().WithMessage("Etkinlik son başvuru tarihi seçiniz");

            RuleFor(a => a.City).NotNull().WithMessage("Etkinliğin yapılacağı şehri seçiniz")
                                .MaximumLength(20).WithMessage("Maksimum 20 karakter olmalıdır");

            RuleFor(a => a.Address).NotNull().WithMessage("Adres boş geçilemez")
                                   .MaximumLength(300).WithMessage("Maksimum 300 karakter olmalıdır");

            RuleFor(a => a.Quota).NotNull().WithMessage("Kontenjan bilgisi boş geçilemez");
        }
    }
}
