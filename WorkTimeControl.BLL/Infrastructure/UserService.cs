using System.Collections;
using System.Collections.Generic;
using WorkTimeControl.BLL.Infrastructure.Interfaces;
using WorkTimeControl.BLL.Mappers;
using WorkTimeControl.BLL.Models;
using WorkTimeControl.DATA.Models;
using WorkTimeControl.DATA.Repositories.Abstract;

namespace WorkTimeControl.BLL.Infrastructure
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public int Create(UserDTO user)
        {
          return  _userRepository.Create(user.ToUser());
        }

        public bool Delete(int Id)
        {
           return _userRepository.Delete(Id);
        }

        public IEnumerable GetAllUsers()
        {
            List<UserDTO> _list = new List<UserDTO>();
            foreach(User user in _userRepository.GetAllUsers())
            {
                _list.Add(user.ToUserDto());
            }           
            return _list;
        }

        public UserDTO GetUserById(int id)
        {           
            UserDTO userDTO = new UserDTO();
            userDTO.Id = _userRepository.GetUserById(id).Id;
            userDTO.Name = _userRepository.GetUserById(id).Name;
            return userDTO;
        }
    }
}
