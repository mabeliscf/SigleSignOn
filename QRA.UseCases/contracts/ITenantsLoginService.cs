using QRA.Entities.Entities;

namespace QRA.UseCases.contracts
{
    public interface ITenantsLoginService
    {
        long create(TenantsLogin tenantsLogin);
    }
}