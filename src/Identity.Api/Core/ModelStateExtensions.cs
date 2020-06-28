using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Identity.Api.Core
{
    public static class ModelStateExtensions
    {
        public static object BadRequest(this ModelStateDictionary modelState)
        {
            return new { Title = "", Errors = new SerializableError(modelState) };
        }

        public static void AddIdentityErrors(this ModelStateDictionary modelState, IdentityResult identityResult)
        {
            if (identityResult.Succeeded)
            {
                return;
            }

            foreach (var error in identityResult.Errors)
            {
                modelState.AddModelError(error.Code, error.Description);
            }
        }
    }
}
