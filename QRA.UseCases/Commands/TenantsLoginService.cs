using QRA.Entities.Entities;
using QRA.Persistence;
using QRA.UseCases.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.Commands
{
    public class TenantsLoginService : ITenantsLoginService
    {

        private QRAchallengeContext _context;

        public TenantsLoginService(QRAchallengeContext qR)
        {
            _context = qR;
        }

        public long create(TenantsLogin tenantsLogin)
        {
            try
            {
                _context.TenantsLogins.Add(tenantsLogin);
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return tenantsLogin.IdTenant;
        }

        public bool update(TenantsLogin tenantsLogin)
        {
            try
            {
                _context.Entry(tenantsLogin).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }

        public bool delete(TenantsLogin tenantsLogin)
        {
            try
            {
                _context.Entry(tenantsLogin).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }

    }
}
