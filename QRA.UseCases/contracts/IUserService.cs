using QRA.Entities.Entities;
using QRA.Entities.Models;
using QRA.UseCases.DTOs;

namespace QRA.UseCases.contracts
{
    public interface IUserService
    {
        Tenant Authenticate(string  username, string password);
        Tenant CreateAdmin(RegisterDTO registerDTO);

        GlobalResponse CreateUser(RegisterUserDTO registerUser);

        bool isNewUser(string mail);
    }
}