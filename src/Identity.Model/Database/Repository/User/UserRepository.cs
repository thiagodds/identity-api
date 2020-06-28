using Identity.Model.Database.Core;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;

namespace Identity.Model.Database.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        public bool HasUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            return _databaseContext.Users.Any(x => x.Email == email);
        }
    }
}
