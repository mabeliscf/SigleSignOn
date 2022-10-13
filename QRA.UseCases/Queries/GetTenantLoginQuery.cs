using AutoMapper;
using QRA.Persistence;
using QRA.UseCases.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.Queries
{
    public class  GetTenantLoginQuery : IGetTenantLoginQuery
    {

        private readonly IMapper imapper;


        private QRAchallengeContext _context;

        public GetTenantLoginQuery(IMapper mapper, QRAchallengeContext qR)
        {
            _context = qR;
            imapper = mapper;
          
        }

        /// <summary>
        /// given a user id, determine if is admin or not 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool isAdmin(long id)
        {
            return _context.TenantsLogins.Where(a => a.IdTenant == id).Select(a => a.Administrator).FirstOrDefault();
        }

        /// <summary>
        /// given a user id determine Tenant father
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public long getTenantFather(long id)
        {
            return (long)_context.TenantsLogins.Where(a => a.IdTenant == id).Select(a => a.TenantFather).FirstOrDefault();
        }

        public bool adminExist()
        {
            return _context.TenantsLogins.Where(a => a.Administrator==true ).Count() ==0 ? false : true;
        }
    }
}
