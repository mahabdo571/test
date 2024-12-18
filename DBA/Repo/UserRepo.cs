using DBA.Entities;
using DBA.Repo.IRepo;
using Microsoft.AspNetCore.Identity;
using Shared.Responses;



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

        public async Task<UserManagerResponse> LoginUserAsync(Users model,string password)
        {
            if(model is null)
                throw new ArgumentNullException("user is null");
            
            if(model.Email is null)
                throw new ArgumentNullException("email is null");

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
                return new UserManagerResponse
                {
                    Message = "User is not found",
                    IsSuccess = false
                };

            var result = await _userManager.CheckPasswordAsync(user, password);

            if (!result)
                return new UserManagerResponse
                {
                    Message = "Password wrong",
                    IsSuccess = false
                };

            return new UserManagerResponse
            {
                Message = "Login Sucssed",
                IsSuccess = true,
            };

      



        }
    }
}
