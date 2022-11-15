using MangaFatihi.Application.Constants;
using MangaFatihi.Application.Models.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MangaFatihi.WebApi.Filters
{
    public class GlobalValidationFilter : IAsyncActionFilter
    {
        public readonly record struct ModelStateErrors(string FieldName, string Message);

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
               .Where(x => x.Value != null && x.Value?.Errors.Count > 0)
               .ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage)).ToArray();

                var errorResponse = new List<ModelStateErrors>();

                foreach (var error in errorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        var errorModel = new ModelStateErrors
                        {
                            FieldName = error.Key,
                            Message = subError
                        };

                        errorResponse.Add(errorModel);
                    }
                }

                context.Result = new UnprocessableEntityObjectResult(
                    new DataResult<List<ModelStateErrors>>(
                            errorResponse,
                            (int)HttpStatusCode.UnprocessableEntity,
                            ApplicationMessages.ErrorModelStateValidation.GetMessage(),
                            ApplicationMessages.ErrorModelStateValidation
                        )
                    );
                return;

            }
            await next();
        }
    }
}
