using System.Collections;
using WorkTimeControl.BLL.Models;

namespace WorkTimeControl.BLL.Infrastructure.Interfaces
{
   public interface IUserTimeService
    {
        int StartTimeCreate(UserTimeDTO user);
        int StopTimeCreate(UserTimeDTO user);
        IEnumerable GetUserTimes(int id);
        IEnumerable GetAllUserTime();
        bool Delete(int userId);
    }
}
