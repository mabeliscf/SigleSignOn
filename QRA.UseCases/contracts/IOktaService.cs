using QRA.Entities.Models;
using QRA.Entities.oktaModels;

namespace QRA.UseCases.contracts
{
    public interface IOktaService
    {
        string AddUsertoGroup(long groupID, long userid);
        string CreateGroups(oktaGroup body);
        string CreateUser(OktaUser body);
        string CreateUserGroup(OktaUserGroup body);
        string DeleteUsertoGroup(long groupID, long userid);
        OktaToken getToken();
    }
}
