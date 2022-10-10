

using QRA.Entities.Entities;

namespace QRA.UseCases.contracts
{
    /// <summary>
    /// crud actions of tenants table
    /// </summary>
    public interface ITenantService
    {
        long create(Tenant tenant);
        bool update(Tenant tenant);
        bool delete(Tenant tenant);
        void createTenantRole(long idTenant, long idRole);
        bool updateTenantRole(TenantsRole tenantsRole);
        bool deleteTenantRole(TenantsRole tenantsRole);
        void createTenantDBAccess(long idTenant, long idDB);
        bool updateTenantDBAccess(DbAccess dbAccess);
        bool deleteTenantDBAccess(DbAccess dbAccess);
    }
}