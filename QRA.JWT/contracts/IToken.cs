

using QRA.Entities.Entities;
using QRA.Entities.Models;

namespace QRA.JWT.contracts
{
    public interface IToken
    {
        GlobalResponse validateUser(Tenant login);
    }
}