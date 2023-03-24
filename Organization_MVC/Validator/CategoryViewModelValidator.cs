using FluentValidation;
using Organization_MVC.ViewModels;

namespace Organization_MVC.Validator
{
    public class CategoryViewModelValidator : AbstractValidator<CategoryViewModel>
    {
        public CategoryViewModelValidator()
        {
            RuleFor(c => c.Name).NotNull().WithMessage("Kategori boş bırakılamaz")
                                .MaximumLength(40).WithMessage("Maksimum 40 karakter olmalıdır");
        }
    }
}
