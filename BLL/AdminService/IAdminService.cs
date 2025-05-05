using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AdminService
{
    public interface IAdminService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(int id);
        Task AddUserAsync(UserDTO userDto, string plainPassword);
        Task UpdateUserAsync(UserDTO userDto);
        Task DeleteUserAsync(int id);

        Task<IEnumerable<CargoTypeDTO>> GetAllCargoTypesAsync();
        Task AddCargoTypeAsync(CargoTypeDTO dto);
        Task UpdateCargoTypeAsync(CargoTypeDTO dto);
        Task DeleteCargoTypeAsync(int id);

        Task<IEnumerable<UserDTO>> SearchUsersByLoginAsync(string loginPart);
        Task<bool> ChangeUserPasswordAsync(int userId, string newPassword);

    }

}
