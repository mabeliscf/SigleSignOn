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
    public  class RolesService : IRolesService
    {
        private QRAchallengeContext _context;

        public RolesService(QRAchallengeContext qR)
        {
            _context = qR;
        }

        public long create(Role role)
        {
            try
            {
                _context.Roles.Add(role);
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return role.IdRole;
        }

        public bool update(Role role)
        {
            try
            {
                _context.Entry(role).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }

        public bool delete(Role role)
        {
            try
            {
                _context.Entry(role).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
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
