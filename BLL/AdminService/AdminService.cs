using AutoMapper;
using BLL.DTOs;
using DAL.Models;
using DAL.UoW;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AdminService(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Users.GetAllWithRoleAsync(); // метод с Include(Role)
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UserDTO>(user);
        }

        public async Task AddUserAsync(UserDTO dto, string plainPassword)
        {
            var user = _mapper.Map<User>(dto);
            user.PasswordHash = _passwordHasher.HashPassword(user, plainPassword);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateUserAsync(UserDTO dto)
        {
            var existing = await _unitOfWork.Users.GetByIdAsync(dto.UserId);
            if (existing == null) return;

            _mapper.Map(dto, existing);
            _unitOfWork.Users.Update(existing);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user != null)
            {
                _unitOfWork.Users.Delete(user);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<IEnumerable<CargoTypeDTO>> GetAllCargoTypesAsync()
        {
            var list = await _unitOfWork.CargoTypes.GetAllAsync();
            return _mapper.Map<IEnumerable<CargoTypeDTO>>(list);
        }

        public async Task AddCargoTypeAsync(CargoTypeDTO dto)
        {
            var entity = _mapper.Map<CargoType>(dto);
            await _unitOfWork.CargoTypes.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateCargoTypeAsync(CargoTypeDTO dto)
        {
            var entity = await _unitOfWork.CargoTypes.GetByIdAsync(dto.CargoTypeId);
            if (entity == null) return;
            _mapper.Map(dto, entity);
            _unitOfWork.CargoTypes.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCargoTypeAsync(int id)
        {
            var entity = await _unitOfWork.CargoTypes.GetByIdAsync(id);
            if (entity != null)
            {
                _unitOfWork.CargoTypes.Delete(entity);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<IEnumerable<UserDTO>> SearchUsersByLoginAsync(string loginPart)
        {
            var users = await _unitOfWork.Users.GetAllWithRoleAsync();
            return users
                .Where(u => u.Login.Contains(loginPart, StringComparison.OrdinalIgnoreCase))
                .Select(u => _mapper.Map<UserDTO>(u));
        }

        public async Task<bool> ChangeUserPasswordAsync(int userId, string newPassword)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                return false;

            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();
            return true;
        }


    }

}
