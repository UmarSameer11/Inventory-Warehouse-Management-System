using FluentValidation;
using WarehouseManagementSystemApi.DTOs.VehicleAssignment;

namespace WarehouseManagementSystemApi.Validators.VehicleAssignment
{
    public class VehicleAssignmentCreateValidator
        : AbstractValidator<VehicleAssignmentCreateDto>
    {
        public VehicleAssignmentCreateValidator()
        {
            RuleFor(x => x.VehicleId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Vehicle.");

            RuleFor(x => x.EmployeeId)
                .GreaterThan(0)
                .WithMessage("Please select a valid Employee (Driver/Salesman).");

            RuleFor(x => x.AssignmentDate)
                .NotEmpty()
                .WithMessage("Assignment Date is required.");
        }
    }
}
