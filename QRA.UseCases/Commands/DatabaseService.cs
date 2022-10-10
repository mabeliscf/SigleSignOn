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
    public  class DatabaseService : IDatabaseService
    {

        private QRAchallengeContext _context;

        public DatabaseService(QRAchallengeContext qR)
        {
            _context = qR;
        }

        public long create(Db db)
        {
            try
            {
                _context.Dbs.Add(db);
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return db.IdDb;
        }

        public bool update(Db db)
        {
            try
            {
                _context.Entry(db).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }

        public bool delete(Db db)
        {
            try
            {
                _context.Entry(db).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
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
