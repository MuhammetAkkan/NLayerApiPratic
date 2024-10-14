using FluentValidation;

namespace App.Service.Categories.Update;

public class UpdateRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateRequestValidator()
    {
        RuleFor(i => i.Name)
            .NotNull().WithMessage($"Kategori ismi null olamaz.")
            .NotEmpty().WithMessage($"Kategori ismi boş olamaz.")
            .Length(1, 100).WithMessage($"Kategori ismi 1 ile 100 karakter arasında olabilir.")
            .Must(name => !name.StartsWith(" ")).WithMessage("Kategori ismi boşluk ile başlayamaz.")
            .Must(name => !name.EndsWith(" ")).WithMessage("Kategori ismi boşluk ile bitemez.")
            .Must(name => name.All(char.IsLetterOrDigit)).WithMessage("Kategori ismi sadece harf ve rakam içermelidir.");
    }
}