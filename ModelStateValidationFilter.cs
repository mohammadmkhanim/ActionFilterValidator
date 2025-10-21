using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilterValidator;

public class ModelStateValidationFilter(IServiceProvider serviceProvider) : IActionFilter
{
    private readonly IServiceProvider _serviceProvider =
        serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

        foreach (var parameter in descriptor!.Parameters)
        {
            if (_serviceProvider.GetService(typeof(IValidator<>).MakeGenericType(parameter.ParameterType)) is not
                IValidator validator)
            {
                continue;
            }

            var postedData = context.ActionArguments.FirstOrDefault(a => a.Key == parameter.Name).Value;
            if (postedData == null)
            {
                context.Result = new JsonResult(DefaultRes.Fail("Posted data is null.(invalid structure)"));
                return;
            }

            var validationContext = new ValidationContext<object>(postedData);
            var validationResult = validator.Validate(validationContext);
            if (!validationResult.IsValid)
            {
                context.Result = new JsonResult(DefaultRes.Fail("Validation failed",
                    validationResult.Errors.Select(x => x.ErrorMessage).ToList()));
                return;
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}