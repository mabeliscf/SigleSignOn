


using QRA.Entities.Entities;
using QRA.Entities.Models;
using QRA.Persistence;
using QRA.UseCases.contracts;
using QRA.UseCases.DTOs;

namespace QRA.UseCases.commands
{
    /// <summary>
    ///  crud actions of tenants table
    /// </summary>
    public class TenantService : ITenantService
    {
        private QRAchallengeContext _context;

        public TenantService(QRAchallengeContext qR )
        {
            _context = qR;
        }
        #region TENANT
        public long create(Tenant tenant)
        {

            try
            {
                _context.Tenants.Add(tenant);
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

            return tenant.IdTenant;
        }

        public bool update(Tenant tenant)
        {
            try
            {
                _context.Entry(tenant).State= Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }

        public bool delete (Tenant tenant)
        {
            try
            {
                _context.Entry(tenant).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;

        }

        #endregion

        #region TENANT_ROLES
        public void createTenantRole(long idTenant, long idRole)
        {
            TenantsRole tenantsRole = new TenantsRole();
            tenantsRole.IdTenant = idTenant;
            tenantsRole.IdRole= idRole;

            try
            {
                _context.TenantsRoles.Add(tenantsRole);
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public bool updateTenantRole(TenantsRole tenantsRole)
        {
            try
            {
                _context.Entry(tenantsRole).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }

        public bool deleteTenantRole(TenantsRole tenantsRole)
        {
            try
            {
                _context.Entry(tenantsRole).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }
        #endregion

        #region DB_ACCESS
        public void createTenantDBAccess(long idTenant, long idDB)
        {
            DbAccess dbAccess = new DbAccess();
            dbAccess.IdTenant = idTenant;
            dbAccess.IdDb = idDB;

            try
            {
                _context.DbAccesses.Add(dbAccess);
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public bool updateTenantDBAccess(DbAccess dbAccess)
        {
            try
            {
                _context.Entry(dbAccess).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }

        public bool deleteTenantDBAccess(DbAccess dbAccess)
        {
            try
            {
                _context.Entry(dbAccess).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }


        #endregion
    }
}
