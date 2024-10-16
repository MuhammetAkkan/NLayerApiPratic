﻿using App.Repositories.Models.Products;
using FluentValidation;

namespace App.Service.Products.Create;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{

    public CreateProductRequestValidator(IProductRepository productRepository)
    {
        RuleFor(i => i.Name)
            .NotNull().WithMessage($"Ürün ismi gereklidir.")
            .NotEmpty().WithMessage($"Ürün ismi boş olamaz gereklidir")
            .Length(1, 100).WithMessage($"Ürün ismi 1 ile 100 karakter arasında olabilir.")
            //.Must(MustUniqueProductName).WithMessage("Ürün ismi veri tabanında bulunmaktadır.");
            ;

        //Price Validation
        RuleFor(i => i.Price)
            .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(1000000).WithMessage("Fiyat en fazla 1,000,000 olabilir.")
            .NotEmpty().WithMessage("Fiyat boş olamaz.");


        //stock
        RuleFor(i => i.UnitStock)
            .GreaterThan(-1).WithMessage("Stock negatif olamaz")
            .Must(i => i % 1 == 0).WithMessage("Stock tam sayı olmalıdır.");



    }
}
