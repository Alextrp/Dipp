using AutoMapper;
using BLL.DTOs;
using DAL.Models;
using DAL.UoW;
using Microsoft.AspNetCore.Identity;

namespace BLL.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _uow;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<bool> RegisterAsync(RegisterDTO dto)
        {
            var existing = await _uow.Users.GetAllAsync();
            if (existing.Any(u => u.Login == dto.Login || u.Email == dto.Email))
                return false;

            var roleId = await _uow.Roles.GetRoleIdByNameAsync("Администратор");
            if (roleId == null)
                throw new Exception("Роль 'Грузоотправитель' не найдена");

            // Маппим RegisterDTO в User с помощью AutoMapper
            var user = _mapper.Map<User>(dto);

            // Присваиваем роль
            user.RoleId = roleId.Value;
            user.Role = null;

            // Хеширование пароля
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            await _uow.Users.AddAsync(user);
            await _uow.SaveAsync();
            return true;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _uow.Users.GetByLoginAsync(username);
            if (user == null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success ? user : null;
        }
    }
}
