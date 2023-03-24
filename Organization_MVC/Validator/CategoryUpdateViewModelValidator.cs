using FluentValidation;
using Organization_MVC.ViewModels;

namespace Organization_MVC.Validator
{
    public class CategoryUpdateViewModelValidator : AbstractValidator<CategoryUpdateViewModel>
    {
        public CategoryUpdateViewModelValidator()
        {
            RuleFor(c => c.OldName).NotNull().WithMessage("Güncellenecek kategori adı boş geçilemez")
                                   .MaximumLength(40).WithMessage("Maksimum 40 karakter olmalıdır");

            RuleFor(c => c.NewName).NotNull().WithMessage("Yeni kategori adı boş geçilemez")
                                   .MaximumLength(40).WithMessage("Maksimum 40 karakter olmalıdır");
        }
    }
}
