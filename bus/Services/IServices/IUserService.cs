using Shared.DTOs;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Services.IServices
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterDTO mopdel);
        Task<UserManagerResponse> LoginUserAsync(LoginDTO model);


    }
}
