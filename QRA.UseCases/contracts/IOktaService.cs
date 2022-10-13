using QRA.Entities.Models;
using QRA.Entities.oktaModels;
using QRA.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.contracts
{
    public interface IOktaService
    {
        object CreateUser(RegisterDTO body);
        OktaToken getToken();
    }
}
