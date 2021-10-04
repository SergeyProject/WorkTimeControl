using System.Collections;
using WorkTimeControl.BLL.Infrastructure.Interfaces;
using WorkTimeControl.BLL.Mappers;
using WorkTimeControl.BLL.Models;
using WorkTimeControl.DATA.Repositories.Abstract;

namespace WorkTimeControl.BLL.Interfaces
{
    public class UserTimeService : IUserTimeService
    {
        IUserTimeRepository _userTimeRepository;
        public UserTimeService(IUserTimeRepository userTimeRepository)
        {
            _userTimeRepository = userTimeRepository;
        }
        public bool Delete(int userId)
        {
            return _userTimeRepository.Delete(userId);
        }

        public IEnumerable GetAllUserTime()
        {
            return _userTimeRepository.GetAllUserTime();
        }

        public IEnumerable GetUserTimes(int id)
        {
            return _userTimeRepository.GetUserTimes(id);
        }

        public int StartTimeCreate(UserTimeDTO user)
        {
            return _userTimeRepository.StartTimeCreate(user.ToUserTime());
        }

        public int StopTimeCreate(UserTimeDTO user)
        {
            return _userTimeRepository.StopTimeCreate(user.ToUserTime());
        }
    }
}
