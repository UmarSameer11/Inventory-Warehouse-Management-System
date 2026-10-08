using FluentValidation;
using WarehouseManagementSystemApi.DTOs.Customer;

namespace WarehouseManagementSystemApi.Validators.Customer
{
    public class CustomerCreateValidator
        : AbstractValidator<CustomerCreateDto>
    {
        public CustomerCreateValidator()
        {
            RuleFor(x => x.CustomerCode)
                .NotEmpty()
                .WithMessage("Customer Code is required.")
                .MaximumLength(30)
                .WithMessage("Customer Code cannot exceed 30 characters.");

            RuleFor(x => x.CustomerName)
                .NotEmpty()
                .WithMessage("Customer Name is required.")
                .MaximumLength(150)
                .WithMessage("Customer Name cannot exceed 150 characters.");

            RuleFor(x => x.ContactPerson)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.ContactPerson))
                .WithMessage("Contact Person cannot exceed 100 characters.");

            RuleFor(x => x.Phone)
                .MaximumLength(30)
                .When(x => !string.IsNullOrWhiteSpace(x.Phone))
                .WithMessage("Phone cannot exceed 30 characters.");
            RuleFor(x => x.Phone)
                .Matches(@"^\+?[0-9\-\s()]{7,20}$")
                .When(x => !string.IsNullOrWhiteSpace(x.Phone))
                .WithMessage("Please enter a valid phone number.");

            RuleFor(x => x.Address)
                .MaximumLength(300)
                .When(x => !string.IsNullOrWhiteSpace(x.Address))
                .WithMessage("Address cannot exceed 300 characters.");

            RuleFor(x => x.City)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.City))
                .WithMessage("City cannot exceed 100 characters.");
        }
    }
}
