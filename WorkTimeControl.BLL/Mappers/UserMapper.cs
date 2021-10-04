using WorkTimeControl.BLL.Models;
using WorkTimeControl.DATA.Models;

namespace WorkTimeControl.BLL.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this UserDTO user)
        {
            return new User { Id = user.Id, Name = user.Name };
        }

        public static UserTime ToUserTime(this UserTimeDTO user)
        {
            return new UserTime
            {
                Id = user.Id,
                DateTimes = user.DateTimes,
                Descript = user.Descript,
                Photo = user.Photo,
                UserId = user.UserId
            };
        }

        public static UserDTO ToUserDto(this User user)
        {
            return new UserDTO { Id = user.Id, Name = user.Name };
        }
    }
}
