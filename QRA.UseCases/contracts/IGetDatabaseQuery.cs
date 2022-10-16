using QRA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.contracts
{
    public interface IGetDatabaseQuery
    {
        List<Db> GetDatabse();
        Db GetDatabasebyId(long id);
        List<Db> GetDatabasebyUser(long id);
    }
}
