using FluentValidation;
using Logic.Models;

namespace Logic.Validators
{
    public class AppointmentValidator : AbstractValidator<AppointmentModel>
    {
        public AppointmentValidator()
        {
            RuleFor(a => a.AppointmentDate)
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("Appointment date must be in the future.");
            RuleFor(a => a.ServiceId)
                .GreaterThan(0)
                .WithMessage("Service ID must be greater than 0.");
            RuleFor(a => a.CustomerId)
                .GreaterThan(0)
                .WithMessage("Customer ID must be greater than 0.");
            RuleFor(a => a.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(100)
                .WithMessage("Title cannot exceed 100 characters.");
        }   
    }
}
