using FluentValidation;
using WarehouseManagementSystemApi.DTOs.Supplier;

namespace WarehouseManagementSystemApi.Validators.Supplier
{
    public class SupplierUpdateValidator
        : AbstractValidator<SupplierUpdateDto>
    {
        public SupplierUpdateValidator()
        {
            RuleFor(x => x.SupplierId)
                .GreaterThan(0)
                .WithMessage("Supplier ID must be greater than 0.");

            RuleFor(x => x.SupplierCode)
                .NotEmpty()
                .WithMessage("Supplier Code is required.")
                .MaximumLength(30)
                .WithMessage("Supplier Code cannot exceed 30 characters.");

            RuleFor(x => x.SupplierName)
                .NotEmpty()
                .WithMessage("Supplier Name is required.")
                .MaximumLength(150)
                .WithMessage("Supplier Name cannot exceed 150 characters.");

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

            RuleFor(x => x.Email)
                .MaximumLength(150)
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("Email cannot exceed 150 characters.");
            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("Please enter a valid email address.");

            RuleFor(x => x.Address)
                .MaximumLength(300)
                .When(x => !string.IsNullOrWhiteSpace(x.Address))
                .WithMessage("Address cannot exceed 300 characters.");
        }
    }
}
