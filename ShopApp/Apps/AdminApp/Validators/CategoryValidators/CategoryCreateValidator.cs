﻿using FluentValidation;
using ShopApp.Apps.AdminApp.Dtos.CategoryDto;


namespace ShopApp.Apps.AdminApp.Validators.CategoryValidators
{
    public class CategoryCreateValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateValidator()
        {
            RuleFor(c => c.Name)
              .NotEmpty().WithMessage("Name is required")
              .MaximumLength(50).WithMessage("Maximum length is 50");

            RuleFor(c => c.Image)
                .NotNull().WithMessage("Image can't be empty")
                .Must(img => img.ContentType.Contains("images/")).WithMessage("Invalid image type..")
                .Must(img => img.Length / 1024 > 1000).WithMessage("Invalid image size");
        }

    }
}