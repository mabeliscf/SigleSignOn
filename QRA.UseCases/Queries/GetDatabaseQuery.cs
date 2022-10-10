using QRA.Entities.Entities;
using QRA.Persistence;
using QRA.UseCases.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.Queries
{
    public  class GetDatabaseQuery: IGetDatabaseQuery
    {

        private QRAchallengeContext _context;

        public GetDatabaseQuery(QRAchallengeContext qR)
        {
            _context = qR;
        }

        public List<Db> GetDatabse()
        {
            return _context.Dbs.ToList();
        }

        public Db GetDatabasebyId(long id)
        {
            return _context.Dbs.Where(a => a.IdDb == id).FirstOrDefault();
        }

        public List<Db> GetDatabasebyUser(long id)
        {
            return (from a in _context.Dbs
                    join b in _context.DbAccesses
                    on a.IdDb equals b.IdDb
                    where b.IdTenant==id
                    select a).ToList();
        }
    }
}
