using FluentValidation;
using Logic.Models;

namespace Logic.Validators
{
    public class ServiceValidator : AbstractValidator<ServiceModel>
    {
        public ServiceValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name cannot exceed 100 characters.");
            RuleFor(s => s.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");
            RuleFor(s => s.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater than or equal to 0.");
            RuleFor(s => s.DurationInMinutes)
                .GreaterThan(0)
                .WithMessage("Duration must be greater than 0 minutes.");
        }
    }
}
