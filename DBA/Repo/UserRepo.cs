using DBA.Entities;
using DBA.Repo.IRepo;
using Microsoft.AspNetCore.Identity;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBA.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<Users> _userManager;

        public UserRepo(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserManagerResponse> RegisterUserRepoAsync(Users model,string password)
        {
            if (model is null)
                throw new NullReferenceException("Register model is Null");




            var result = await _userManager.CreateAsync(model, password);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "User Created Successfully",
                    IsSuccess = true,
                 
                };
            }

            return new UserManagerResponse
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(d => d.Description)
            };
        }
    }
}
