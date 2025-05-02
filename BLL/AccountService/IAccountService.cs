using BLL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AccountService
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(RegisterDTO dto);
        Task<User?> AuthenticateAsync(string username, string password);
    }
}
