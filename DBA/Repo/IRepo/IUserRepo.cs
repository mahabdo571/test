using DBA.Entities;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBA.Repo.IRepo
{
    public interface IUserRepo
    {
      Task<UserManagerResponse> RegisterUserRepoAsync(Users model, string password);
    }
}
