
using AutoMapper;
using QRA.Entities.Entities;
using QRA.Entities.Models;
using QRA.Persistence;
using QRA.UseCases.contracts;

namespace QRA.UseCases.Queries
{
    public  class UserQuery : IUserQuery
    {

        private readonly IMapper imapper;
        private readonly IGetRolesQuery iroles;
        private readonly IGetDatabaseQuery idatabase;
        private readonly IGetTenantQuery itenantQuery;
        private readonly IGetTenantLoginQuery itenantlogin;


        private QRAchallengeContext _context;

        public UserQuery(IMapper mapper, QRAchallengeContext qR, IGetRolesQuery roles, IGetDatabaseQuery database, IGetTenantQuery tenantQuery, IGetTenantLoginQuery tenantLoginQuery)
        {
            _context = qR;
            imapper = mapper;
            iroles = roles;
            idatabase = database;
            itenantQuery = tenantQuery;
            itenantlogin = tenantLoginQuery;
        }



         /// <summary>
         /// get admin info by id, only basic data
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        public AdminInfo GetAdminbyId(long id)
        {
             Tenant tenant = itenantQuery.GetTenantbyId(id);
             AdminInfo admin = imapper.Map<AdminInfo>(tenant);
              admin.IsAdmin = itenantlogin.isAdmin(id); 

            return admin;
        }
        /// <summary>
        /// Get all admins
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool GetAllAdmins()
        {
         
            return itenantlogin.adminExist();
        }
        /// <summary>
        /// given a admin id, returns all tenants with roles, db access, and users under each tenant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AdminInfo GetAdminWTenants(long id)
        {
            //get admin info
            AdminInfo admin= new AdminInfo();
            Tenant tenant = itenantQuery.GetTenantbyId(id); 
            admin = imapper.Map<AdminInfo>(tenant);
            admin.IsAdmin = itenantlogin.isAdmin(id);
            //get all tenant info
            admin.Tenants = GetAllTenants();

            return admin;

        }
        /// <summary>
        /// Get a tenant with their roles DB and users 
        /// </summary>
        /// <returns></returns>
        public TenantInfo GetTenatInfobyId(long id)
        {
            Tenant tenant = itenantQuery.GetTenantbyId(id);
            TenantInfo tenantInfo = imapper.Map<TenantInfo>(tenant);
            tenantInfo.Roles = iroles.GetRolesbyUser(id);
            tenantInfo.Databases = idatabase.GetDatabasebyUser(id);
            tenantInfo.Users = GetUsersbyTenantID(id);
            tenantInfo.IsAdmin = itenantlogin.isAdmin(id);
            return tenantInfo;
        }
        /// <summary>
        /// get all tenants with roles, db and users under
        /// </summary>
        /// <returns></returns>
        public List<TenantInfo> GetAllTenants()
        {
            List<TenantInfo> tenants = new List<TenantInfo>();
            //get all tenants 
            List<Tenant> tenant = itenantQuery.GetAllTetenants();
            TenantInfo tenantInfo = new TenantInfo();
            tenant.ForEach(a => {

                tenantInfo = imapper.Map<TenantInfo>(a);
                tenantInfo.Roles = iroles.GetRolesbyUser(a.IdTenant);
                tenantInfo.Databases = idatabase.GetDatabasebyUser(a.IdTenant);
                tenantInfo.Users = GetUsersbyTenantID(a.IdTenant);
                tenantInfo.IsAdmin = itenantlogin.isAdmin(a.IdTenant);

                tenants.Add(tenantInfo);
            });

            return tenants;
        }
     
            /// <summary>
            /// get all users under a tenant given the tenant id
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
        public List<UserInfo> GetUsersbyTenantID(long id)
        {
            var users = itenantQuery.GetUsersofTenant(id);
            UserInfo usersinfo = new UserInfo();
            List<UserInfo> userInfos = new List<UserInfo>();
            users.ForEach(a =>
            {
                usersinfo = imapper.Map<UserInfo>(a);
                usersinfo.Roles = iroles.GetRolesbyUser(a.IdTenant);
                usersinfo.Databases = idatabase.GetDatabasebyUser(id);
                usersinfo.IsAdmin = false; //by default a user cannot be an admin
                usersinfo.IdTenantFather = itenantlogin.getTenantFather(a.IdTenant);
                userInfos.Add(usersinfo);
            });

            return userInfos;
        }

        /// <summary>
        /// given a user id return info of the use, roles and db access
        /// </summary>
        /// <returns></returns>
        public UserInfo GetUserInfoByID(long idUser)
        {
            Tenant user = itenantQuery.GetTenantbyId(idUser);
            UserInfo userInfo = imapper.Map<UserInfo>(user);
            userInfo.Roles = iroles.GetRolesbyUser(userInfo.Id);
            userInfo.IdTenantFather = itenantlogin.getTenantFather(idUser);
            userInfo.IsAdmin = itenantlogin.isAdmin(idUser);
            //if father is !=0 then return tenant db, else father db
            userInfo.Databases = idatabase.GetDatabasebyUser(userInfo.IdTenantFather ==0 ? idUser : userInfo.IdTenantFather);
            
            return userInfo;
        }
       
    }
}
