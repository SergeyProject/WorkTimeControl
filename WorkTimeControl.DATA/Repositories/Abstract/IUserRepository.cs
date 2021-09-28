using System.Collections.Generic;
using WorkTimeControl.DATA.Models;

namespace WorkTimeControl.DATA.Repositories.Abstract
{
   public interface IUserRepository
    {
        int Create(User user);
        bool Delete(int Id);
        User GetUserById(int id);
        IEnumerable<User> GetAllUsers();
    }
}
