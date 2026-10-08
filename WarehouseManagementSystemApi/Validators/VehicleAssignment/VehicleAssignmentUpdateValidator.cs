using FluentValidation;
using WarehouseManagementSystemApi.DTOs.VehicleAssignment;

namespace WarehouseManagementSystemApi.Validators.VehicleAssignment
{
    public class VehicleAssignmentUpdateValidator
        : AbstractValidator<VehicleAssignmentUpdateDto>
    {
        public VehicleAssignmentUpdateValidator()
        {
            RuleFor(x => x.VehicleAssignmentId)
                .GreaterThan(0)
                .WithMessage("Vehicle Assignment ID must be greater than 0.");

            RuleFor(x => x.VehicleId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Vehicle.");

            RuleFor(x => x.EmployeeId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Employee (Driver/Salesman).");

            RuleFor(x => x.AssignmentDate)
                .NotEmpty()
                .WithMessage("Assignment Date is required.");

            RuleFor(x => x.ReturnDate)
                .GreaterThanOrEqualTo(x => x.AssignmentDate)
                .When(x => x.ReturnDate.HasValue)
                .WithMessage("Return Date cannot be before Assignment Date.");
        }
    }
}
