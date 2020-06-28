using Identity.Model.Api.Dto.User;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Identity.Model.Api.Validators.User
{
    public interface IUserValidator
    {
        void ValidateEmail(ModelStateDictionary modelStateDictionary, string email);
    }
}
