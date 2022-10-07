using QRA.UseCases.DTOs;

namespace QRA.JWT.interfaces
{
    public interface IToken
    {
        GlobalResponse validateUser(LoginDTO login);
    }
}