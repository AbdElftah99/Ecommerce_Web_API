using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace Ecommerce.Factory
{
    public static class ApiResponseFactory
    {
        public static IActionResult CustomValidationError(ActionContext context)
        {
            // Get All Errors in ModelState
            var errors = context.ModelState
                                .Where(e => e.Value?.Errors.Any() ?? false)
                                .Select(e => new ValidationError
                                {
                                    Field = e.Key,
                                    Errors = e.Value?.Errors.Select(e => e.ErrorMessage).ToList() ?? []
                                });

            // Build My Custom ValidationErrorResult
            var response = new ValidationErrorResult
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Validation Error",
                Errors = errors
            };

            // Return the custom errors
            return new BadRequestObjectResult(response);
        }
    }
}
