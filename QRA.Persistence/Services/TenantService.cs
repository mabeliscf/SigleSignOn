using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRA.Entities.contracts;
using QRA.Entities.Entities;
using QRA.UseCases.DTOs;

namespace QRA.Persistence.Services
{
    public class TenantService : ITenantService
    {
        private QRAchallengeContext entity;

        public TenantService(QRAchallengeContext qR )
        {
            entity = qR;
        }

        

        public Object GetUser(string username, string password, int logintype)
       {
            //return (from a in entity.Tenants
            //            join b in entity.TenantsLogins
            //            on a.IdTenant equals b.IdTenant
            //            where b.Username == username && b.PasswordEncrypted == password && b.LoginType == logintype
            //            select new TeantLoginDTO
            //            {
            //               IdTenant=  a.IdTenant,
            //               Username=  b.Username,
            //               Fullname=  a.Fullname,
            //                Email = a.Email
            //            }).FirstOrDefault();

            TeantLoginDTO userDB= new TeantLoginDTO();
            //delete 
            userDB.Username = "rcorniel";
            userDB.Email = "raemilcorniel@hotmail.com";
            userDB.Fullname = "Raemil Corniel";
            userDB.IdTenant = 1;

            return userDB;

        }

      

        public GlobalResponse addTenant(TenantDTO tenantDTO)
        {
            GlobalResponse response = new GlobalResponse();
            Tenant tenant1 = new Tenant()
            {
                IdTenant = tenantDTO.IdTenant,
                Fullname = tenantDTO.Fullname,
                Email = tenantDTO.Email,
                Phone = tenantDTO.Phone
            };
            entity.Tenants.Add(tenant1);
            int returnValue = entity.SaveChanges();

            response.responseNumber = returnValue;
            if (returnValue > 0)
            {
                response.response = "Data Saved";


            }
            else
            {
                response.response = "Data not Saved";
            }

            return response;
        }

        public Tenant getTenant(Tenant tenant)
        {
            return entity.Tenants.Where(a => a.IdTenant == tenant.IdTenant).Select(a => a).First();

        }
      
    }
}
