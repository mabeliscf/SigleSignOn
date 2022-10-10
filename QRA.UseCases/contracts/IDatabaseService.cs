using QRA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.contracts
{
    public interface IDatabaseService
    {
        long create(Db db);
        bool delete(Db db);
        bool update(Db db);
    }
}
