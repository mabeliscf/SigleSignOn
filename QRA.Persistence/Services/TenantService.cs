using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRA.Entities.Entities;
using QRA.UseCases.DTOs;

namespace QRA.Persistence.Services
{
    public  class TenantService
    {
        private QRAchallengeContext entity;

        public TenantService()
        {
            entity = new QRAchallengeContext();
        }

        public GlobalResponse addTenant(TenantDTO tenantDTO)
        {
            GlobalResponse response= new GlobalResponse();
            Tenant tenant1 = new Tenant()
            {
                IdTenant = tenantDTO.IdTenant,
                Fullname = tenantDTO.Fullname,
                Email = tenantDTO.Email,
                Phone = tenantDTO.Phone
            };
            entity.Tenants.Add(tenant1);
            int returnValue =entity.SaveChanges();

            response.responseNumber = returnValue;
            if (returnValue>0)
            {
                response.response = "Data Saved";
               

            }else
            {
                response.response = "Data not Saved";
            }

            return response;
        }

    }
}
