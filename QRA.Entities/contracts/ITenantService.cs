using QRA.Entities.Entities;

namespace QRA.Entities.contracts
{
    public interface ITenantService
    {
    
        /// <summary>
        /// Get user login data 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="logintype"></param>
        /// <returns></returns>
        Object GetUser(string username, string password, int logintype);
    }
}