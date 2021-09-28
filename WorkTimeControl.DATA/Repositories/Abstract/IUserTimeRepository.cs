using System.Collections;
using WorkTimeControl.DATA.Models;

namespace WorkTimeControl.DATA.Repositories.Abstract
{
   public interface IUserTimeRepository
    {
        int StartTimeCreate(UserTime user);
        int StopTimeCreate(UserTime user);
        IEnumerable GetUserTimes(int id);
        IEnumerable GetAllUserTime();
        bool Delete(int userId);
    }
}
