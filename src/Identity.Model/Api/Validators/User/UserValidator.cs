using Identity.Model.Database.Repository.User;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Identity.Model.Api.Validators.User
{
    public class UserValidator : IUserValidator
    {
        private IUserRepository _userRepository;

        public UserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public void ValidateEmail(ModelStateDictionary modelStateDictionary, string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (_userRepository.HasUserByEmail(email))
            {
                modelStateDictionary.AddModelError("Email", $"There is an user with the Email { email } in the system.");
            }
        }
    }
}
