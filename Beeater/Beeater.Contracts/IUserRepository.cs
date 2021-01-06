using Beeater.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beeater.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetUsersByFullName(string firstname, string lastname);
        Task<User> GetUserByEmail(string email);
        Task DeleteUser(string id);
    }
}
