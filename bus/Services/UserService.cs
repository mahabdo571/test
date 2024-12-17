using AutoMapper;
using BUS.Services.IServices;
using DBA.Entities;
using DBA.Repo.IRepo;
using Shared.DTOs;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterDTO model)
        {
            if (model is null)
                throw new NullReferenceException("Register model is Null");

            if (!model.Password.Equals(model.PasswordConfirm))
                return new UserManagerResponse
                {
                    Message = "confirm passwerd doesn't match the password",
                    IsSuccess = false
                };

     

            try
            {
                var mapModel = _mapper.Map<Users>(model);
                var result = await _userRepo.RegisterUserRepoAsync(mapModel, model.Password);
                return result;

            }
            catch (Exception ex)
            {
                return new UserManagerResponse
                {
                    Message = ex.Message,
                    IsSuccess = false,

                };
            }

        }
    }
}
