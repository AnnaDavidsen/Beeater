using Beeater.Contracts;
using Beeater.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beeater.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(beeaterContext context)
            : base(context)
        {

        }

        public async Task DeleteUser(string id)
        {
            var userToDelete = await FindByCondition(x => x.Id == id).ToListAsync();
            Delete(userToDelete);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await FindByCondition(x => x.Email.ToLower() == email.ToLower())
                .Include(x => x.Bookings)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<IEnumerable<User>> GetUsersByFullName(string firstname, string lastname)
        {
            var user = await FindByCondition(x => x.Firstname.ToLower() == firstname.ToLower()
                && x.Lastname.ToLower() == lastname.ToLower())
                .ToListAsync();

            return user;
        }
    }
}
