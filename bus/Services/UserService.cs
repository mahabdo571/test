using AutoMapper;
using BUS.Services.IServices;
using DBA.Entities;
using DBA.Repo.IRepo;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.DTOs;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepo userRepo, IMapper mapper, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _configuration = configuration;
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
                    IsSuccess = false

                };
            }

        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginDTO model)
        {

            if (model is null)
                throw new NullReferenceException("login model is Null");



            try
            {
                var mapModel = _mapper.Map<Users>(model);
                var result = await _userRepo.LoginUserAsync(mapModel, model.Password);
                if (result.IsSuccess)
                    return _genratorToken(mapModel);

                return new UserManagerResponse
                {
                    Message = "login filed",
                    IsSuccess = false,


                };
            }
            catch (Exception ex)
            {
                return new UserManagerResponse
                {
                    Message = ex.Message,
                    IsSuccess = false

                };
            }


        }

        private UserManagerResponse _genratorToken(Users model)
        {
            if (model is null)
                throw new ArgumentNullException("model is null");

            var authKey = _configuration["AuthSettings:Key"];
            var issuer = _configuration["AuthSettings:Issuer"];
            var audience = _configuration["AuthSettings:Audience"];
            var day = _configuration["AuthSettings:expiresDay"];


            if (authKey is null || audience is null || issuer is null || day is null)
                throw new ArgumentNullException(" missing in configuration");


            if (!int.TryParse(day, out int resultDay))
                throw new Exception("convert day error");


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authKey));

            var claims = new[] {
            new Claim("Email",model.Email!),
            new Claim(ClaimTypes.NameIdentifier,model.Id)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(resultDay),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = tokenString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };

        }
    }
}
