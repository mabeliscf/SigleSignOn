
using AutoMapper;
using QRA.Entities.Entities;
using QRA.Entities.Models;
using QRA.Entities.oktaModels;
using QRA.Persistence;
using QRA.UseCases.contracts;
using QRA.UseCases.DTOs;
using System.Text;
using Profile = QRA.Entities.oktaModels.Profile;

namespace QRA.UseCases.commands
{
    public class UserService : IUserService
    {
        private QRAchallengeContext _context;
        private readonly IMapper imapper;
        private readonly ITenantsLoginService tenantsLoginService;
        private readonly ITenantService tenantService;
        private readonly IOktaService iokta;
        private readonly IGetTenantQuery iTenantQuery;
        private readonly IUserQuery iuserQuery;


        private readonly IGetTenantLoginQuery iTenantLoginQuery;



        public UserService(IOktaService okta,QRAchallengeContext qR, IMapper mapper, ITenantsLoginService tenantsLogin, ITenantService tenant, IGetTenantQuery TenantQuery, IGetTenantLoginQuery tenantLoginQuery, IUserQuery userQuery)
        {
            _context = qR;
            imapper = mapper;
            tenantsLoginService = tenantsLogin;
            tenantService = tenant;
            iokta = okta;
            iTenantLoginQuery = tenantLoginQuery;
            iuserQuery = userQuery;
        }

        /// <summary>
        /// validate username and password with database users
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Tenant Authenticate(string username, string password)
        {
            //access db and get user 
            var user = _context.Tenants.Where(a => a.Email == username).FirstOrDefault();

            if (user == null) return null;

            //get password 
            var credential = _context.TenantsLogins.Where(a => a.IdTenant == user.IdTenant && a.LoginType==1).FirstOrDefault();
            if (credential == null) return null;
            //validate password if hass y secret 

            if (!validatePassword(password, credential.PasswordEncrypted, credential.PasswordSalt))
                return null;

            Tenant response = _context.Tenants.Where(a => a.IdTenant == user.IdTenant).First();
            return response;
        }
        
        /// <summary>
        /// identify is user exist
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool isNewUser(string mail)
        {
            if( _context.Tenants.Where(a => a.Email == mail).FirstOrDefault() == null )    
                return true;


            return false;
        }


        /// <summary>
        /// create new user without roles , local admin
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        public GlobalResponse CreateAdmin(RegisterDTO registerDTO)
        {
            GlobalResponse global = new GlobalResponse();
            try
            {
                
                //create user in tenants 
                Tenant register = CreateBasicUser(registerDTO,"");
                global.responseNumber = 1;
                global.response = "Created Succesfull";

                if (register.IdTenant == 0)
                {
                    
                    global.responseNumber = 0;
                    global.response = "Error";
                }

               
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

            return global;
        }

        /// <summary>
        /// creates user with roles and db access if aply
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns></returns>
        public GlobalResponse CreateUser(RegisterUserDTO registerUser)
        {
            GlobalResponse response = new GlobalResponse();

            //identify if is Create or update
            Tenant tenant = iTenantQuery.UserExist(registerUser.Email);
            if (tenant== null)
            {
                //identify if is tenant or user 
                if (registerUser.IsUser)
                {
                    response = CreateUserForTenantSpace(registerUser);
                }
                else
                {
                    response = CreateTenantSpace(registerUser);
                }

            }else //update tenant data 
            {
                //identify if is tenant or user 
                if (registerUser.IsUser)
                {
                    response = UpdateUserForTenantSpace(registerUser);
                }
                else
                {
                    response = UpdateTenantSpace(registerUser);
                }
            }
            return response;
        }
        /// <summary>
        /// create tenant in local and okta, login 
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public GlobalResponse CreateTenantSpace(RegisterUserDTO registerUser)
        {
            GlobalResponse global = new GlobalResponse();
            string groupID = "";

            //only if okta selected
            if (registerUser.Logintype == 2)
            {
                //create group in okta
                 groupID = CreateGroupsOkta(registerUser);
            }
            try
            {
                //if group id exist 
                if (registerUser.Logintype == 2 && groupID != "")
                { //create user in okta with group created 
                    global = CreateOktaUserGroup(registerUser, groupID);

                }
                else 
                    {
                        //create in local db user with okta details
                        Tenant tenant = CreateBasicUser(registerUser, groupID);
                        global.response = "UserCreated";
                        global.responseNumber = 1;
                        if (tenant.IdTenant == 0)
                        {
                            global.response = "Error while creating user";
                            global.responseNumber = 0;
                        }

                        //create DB Reletionship 
                        registerUser.Databases.ForEach(a => {
                            a.IdTenant = tenant.IdTenant;
                            tenantService.createTenantDBAccess(tenant.IdTenant, a.IdDb);

                        });

                        //create roles relation
                        registerUser.Roles.ForEach(a => {
                            a.IdTenant = tenant.IdTenant;
                            tenantService.createTenantRole(tenant.IdTenant, a.IdRole);

                        });
                    }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

            return global;
        }

        /// <summary>
        /// create user under a tenant space and with login type of father
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public GlobalResponse CreateUserForTenantSpace(RegisterUserDTO registerUser)
        {
            GlobalResponse global =new  GlobalResponse();

            //get father to identify type of login 
            Tenant father = iTenantQuery.GetTenantbyId(registerUser.IdTenantFather);

            try
            {
                //if group id exist 
                if (registerUser.Logintype == 2 && father.TenantSpaceID != "")
                { //create user in okta with group created 
                    global = CreateOktaUserGroup(registerUser, father.TenantSpaceID);
                }
                else
                {
                    //create in local db user with okta details
                    Tenant tenant = CreateBasicUser(registerUser, father.TenantSpaceID);
                    global.response = "UserCreated";
                    global.responseNumber = 1;
                    if (tenant.IdTenant == 0)
                    {
                        global.response = "Error while creating user";
                        global.responseNumber = 0;
                    }

                    //create roles relation
                    registerUser.Roles.ForEach(a => {
                        a.IdTenant = tenant.IdTenant;
                        tenantService.createTenantRole(tenant.IdTenant, a.IdRole);

                    });
                }
                
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return global;
        }

       /// <summary>
       /// update tenant and users if login type change 
       /// </summary>
       /// <param name="registerUser"></param>
       /// <returns></returns>
        public GlobalResponse UpdateTenantSpace(RegisterUserDTO registerUser)
        {
            GlobalResponse global = new GlobalResponse();
            string groupid = "";
            TenantInfo tenantinfo = iuserQuery.GetTenatInfobyId(registerUser.id);

            //check if login type change 
            if(registerUser.Logintype!= tenantinfo.LoginType && registerUser.Logintype == 2)
            {
                //move user and tenant to okta 
                groupid = CreateGroupsOkta(registerUser);
                //create user in okta 
                global= CreateOktaUserGroup(registerUser, groupid);

                //get  user of tenant and change type login 
                global = CreateUsersOkta(registerUser.id, groupid);


            }
            else
            {
                //update tenant 
                Tenant tenant = imapper.Map<Tenant>(registerUser);
                tenant.TenantSpaceID = groupid;
                tenantService.update(tenant);

                //update tenant login 
                TenantsLogin tenantsLogin = iTenantLoginQuery.getLoginbyID(registerUser.id);
                if (!validatePassword(registerUser.Password, tenantsLogin.PasswordEncrypted, tenantsLogin.PasswordSalt))
                {
                    byte[] hash, salt;
                    CreateHashPassword(registerUser.Password, out hash, out salt);
                    tenantsLogin.PasswordSalt = salt;
                    tenantsLogin.PasswordEncrypted = hash;
                }
                tenantsLogin.LoginType = registerUser.Logintype;
                tenantsLogin.TenantFather = registerUser.IdTenantFather;
                tenantsLoginService.update(tenantsLogin);

                //update roles
                registerUser.Roles.ForEach(a =>
                {
                    tenantService.updateTenantRole(a);
                });

                if (registerUser.IsUser == false){
                    registerUser.Databases.ForEach(a =>
                    {
                        tenantService.updateTenantDBAccess(a);
                    });
                }

            }
            return global;
        }

        /// <summary>
        /// update user roles and basic info 
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns></returns>
       public GlobalResponse UpdateUserForTenantSpace(RegisterUserDTO registerUser)
        {
            GlobalResponse global = new GlobalResponse();

            TenantInfo tenantinfo = iuserQuery.GetTenatInfobyId(registerUser.id);

            //update tenant 
            Tenant tenant = imapper.Map<Tenant>(registerUser);
            tenantService.update(tenant);

            //update tenant login 
            TenantsLogin tenantsLogin = iTenantLoginQuery.getLoginbyID(registerUser.id);
            if (!validatePassword(registerUser.Password, tenantsLogin.PasswordEncrypted, tenantsLogin.PasswordSalt))
            {
                byte[] hash, salt;
                CreateHashPassword(registerUser.Password, out hash, out salt);
                tenantsLogin.PasswordSalt = salt;
                tenantsLogin.PasswordEncrypted = hash;
            }
           
            tenantsLoginService.update(tenantsLogin);

            //update roles
            registerUser.Roles.ForEach(a =>
            {
                tenantService.updateTenantRole(a);
            });

            if (registerUser.IsUser == false)
            {
                registerUser.Databases.ForEach(a =>
                {
                    tenantService.updateTenantDBAccess(a);
                });
            }
            return global;
        }
        /// <summary>
        /// creat user in okta 
        /// </summary>
        /// <param name="registerUser"></param>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public GlobalResponse CreateOktaUserGroup(RegisterUserDTO registerUser, string groupid)
        {
            GlobalResponse global = new GlobalResponse( );
            //create user in okta with group created 
            OktaUserGroup oktaUserGroup = imapper.Map<OktaUserGroup>(registerUser);
            oktaUserGroup.groupIds[oktaUserGroup.groupIds.Length + 1] = groupid;
            var status = iokta.CreateUserGroup(oktaUserGroup);

            if (status == "LOCKED_OUT" || status == "DEPROVISIONED")
            {
                global.response = "Error while creating user, user Status=" + status;
                global.responseNumber = 0;

            }
            return global;
        }

        /// <summary>
        /// create groups in okta, groups are tenant space
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns></returns>
        public string  CreateGroupsOkta(RegisterUserDTO registerUser)
        {
            //create group in okta
            oktaGroup group = imapper.Map<oktaGroup>(registerUser);
            return  iokta.CreateGroups(group); ;
        }

        //create all users for ockta, user when tentant update
        public GlobalResponse CreateUsersOkta(long id, string grupoid)
        {
            GlobalResponse global = new GlobalResponse();
            //get all users 
            List<UserInfo> tenantInfo = iuserQuery.GetUsersbyTenantID(id);
            RegisterUserDTO registerUser = new RegisterUserDTO();

            try
            {
                tenantInfo.ForEach(user => {
                    registerUser = imapper.Map<RegisterUserDTO>(user);
                    CreateOktaUserGroup(registerUser, grupoid);
                });

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            global.response = "Saved";
            global.responseNumber = 1;

            return global;
        }

        ///// <summary>
        ///// create user and tenant space Group
        ///// </summary>
        ///// <param name="registerUser"></param>
        ///// <returns></returns>
        ///// <exception cref="Exception"></exception>
        //public GlobalResponse CreateTenantGroup(RegisterUserDTO registerUser)
        //{
        //    GlobalResponse response = new GlobalResponse();

        //    try
        //    {
        //        //map model to okta model
        //        OktaUser registerOktaUser = imapper.Map<OktaUser>(registerUser);
        //        registerOktaUser.profile = new Profile();
        //        registerOktaUser.profile = imapper.Map<Profile>(registerUser);
        //        Credentials credentials = new Credentials();
        //        credentials.password = new Password();
        //        credentials.password = imapper.Map<Password>(registerUser);
        //        registerOktaUser.credentials = credentials;
        //        // Create user under a gruop (tenant space )
        //        string status = iokta.CreateUser(body: registerOktaUser);

        //        oktaGroup oktaGroup = imapper.Map<oktaGroup>(registerUser);
        //        string tenant_space_ID  = iokta.CreateGroups(body: oktaGroup);

        //        //TODO:add db space id 
        //        //create user 
        //         Tenant register= CreateBasicUser(registerUser,"");
        //        //create roles
        //        registerUser.Roles.ForEach(role => { tenantService.createTenantRole(register.IdTenant, role.IdRole); });

        //        if (!registerUser.IsUser)
        //        {
        //            //create db
        //            registerUser.Databases.ForEach(role => { tenantService.createTenantDBAccess(register.IdTenant, role.IdDb); });

        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        response.responseNumber = 0;
        //        response.response = e.Message;

        //        throw new Exception(e.Message);
        //    }

        //    response.responseNumber = 1;
        //    response.response = "Creado Correctamente!";
        //    return response;
        //}

        /// <summary>
        /// creates a local user without roles or db access
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private  Tenant CreateBasicUser( RegisterDTO registerUser, string groupid)
        {
            Tenant register = imapper.Map<Tenant>(registerUser);

            try
            {  //create user 
                register.TenantSpaceID = groupid;
                register.IdTenant = tenantService.create(register);
                

                //create access 
                TenantsLogin access = imapper.Map<TenantsLogin>(registerUser);
                //create password 
                byte[] hash, salt;
                CreateHashPassword(registerUser.Password, out hash, out salt);
                access.IdTenant = register.IdTenant;
                access.PasswordEncrypted = hash;
                access.PasswordSalt = salt;
                register.IdTenant = tenantsLoginService.create(access);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return register;
        }

        /// <summary>
        /// validate if user input password is the same as the one storage
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private static bool validatePassword(string password, byte[] hash, byte[] salt)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            using (var sec = new System.Security.Cryptography.HMACSHA512(salt))
            {
                //apply hash to password 
                var gethash = sec.ComputeHash(Encoding.UTF8.GetBytes(password));
                for(int i=0; i< gethash.Length; i++)
                {
                    if (gethash[i] != hash[i]) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// given a clear text password, convert to hash and key
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <param name="salt"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private static void CreateHashPassword(string password,out byte[] hash, out byte[] salt)
        {
            if(password==null) throw new ArgumentNullException("password");

            using (var sec = new System.Security.Cryptography.HMACSHA512())
            {
                salt = sec.Key;
                hash= sec.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
