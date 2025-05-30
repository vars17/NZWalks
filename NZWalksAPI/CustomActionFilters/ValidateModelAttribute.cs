using Microsoft.AspNetCore.Mvc.Filters;

namespace NZWalksAPI.CustomActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //check if model is valid
            if (context.ModelState.IsValid == false)
            {
                //if model is not valid, return bad request
                context.Result = new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(context.ModelState);

            }


        }
    }
}
