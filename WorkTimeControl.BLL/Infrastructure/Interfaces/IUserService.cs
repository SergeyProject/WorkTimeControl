using System.Collections;
using System.Collections.Generic;
using WorkTimeControl.BLL.Models;

namespace WorkTimeControl.BLL.Infrastructure.Interfaces
{
   public interface IUserService
    {
        int Create(UserDTO user);
        bool Delete(int Id);
        UserDTO GetUserById(int id);
        IEnumerable GetAllUsers();
    }
}
