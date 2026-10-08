using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WarehouseManagementSystemApi.Filters
{
    /// <summary>
    /// Runs the FluentValidation validator (if one is registered) for every action argument.
    /// On failure it throws ArgumentException, which the existing global exception middleware
    /// turns into a 400 ErrorResponse - so validation errors look like every other error.
    /// Apply with [ServiceFilter(typeof(ValidationFilter))].
    /// </summary>
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var errors = new List<string>();

            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument is null)
                {
                    continue;
                }

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());

                if (_serviceProvider.GetService(validatorType) is not IValidator validator)
                {
                    continue;
                }

                var result = await validator.ValidateAsync(
                    new ValidationContext<object>(argument),
                    context.HttpContext.RequestAborted);

                if (!result.IsValid)
                {
                    errors.AddRange(result.Errors.Select(e => e.ErrorMessage));
                }
            }

            if (errors.Count > 0)
            {
                throw new ArgumentException(string.Join(" ", errors.Distinct()));
            }

            await next();
        }
    }
}
