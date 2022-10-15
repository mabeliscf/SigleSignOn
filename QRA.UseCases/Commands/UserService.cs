
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


        public UserService(IOktaService okta,QRAchallengeContext qR, IMapper mapper, ITenantsLoginService tenantsLogin, ITenantService tenant)
        {
            _context = qR;
            imapper = mapper;
            tenantsLoginService = tenantsLogin;
            tenantService = tenant;
            iokta = okta;
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
            var user = _context.Tenants.Where(a => a.Username == username).FirstOrDefault();

            if (user == null) return null;

            //get password 
            var credential = _context.TenantsLogins.Where(a => a.IdTenant == user.IdTenant).FirstOrDefault();
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
        /// create new user without roles 
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        public GlobalResponse CreateAdmin(RegisterDTO registerDTO)
        {
            GlobalResponse global = new GlobalResponse();
            try
            {
                //map model to okta model
                OktaUser registerOktaUser = imapper.Map<OktaUser>(registerDTO);
                registerOktaUser.profile = new Profile();
                registerOktaUser.profile = imapper.Map<Profile>(registerDTO);
                Credentials credentials = new Credentials();
                credentials.password = new Password();
                credentials.password = imapper.Map<Password>(registerDTO);
                registerOktaUser.credentials = credentials;
                // Create user in okta
                string status = iokta.CreateUser(body: registerOktaUser);

                //create user in tenants 
                Tenant register = CreateBasicUser(registerDTO);


                global.response = status;
                global.responseNumber = 1;
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
            
            try
            {
                //TODO: tenant father cannto save null
                //TODO create modelor update 
                //Todo Get user gruop from tenant father db
                //map model to okta model
                OktaUserGroup registerOktaUser = imapper.Map<OktaUserGroup>(registerUser);
                registerOktaUser.profile = new Profile();
                registerOktaUser.profile = imapper.Map<Profile>(registerUser);
                Credentials credentials = new Credentials();
                credentials.password = new Password();
                credentials.password = imapper.Map<Password>(registerUser);
                registerOktaUser.credentials = credentials;
                // Create user under a gruop (tenant space )
                string status = iokta.CreateUserGroup(body: registerOktaUser);

                //create user 
                Tenant register = CreateBasicUser(registerUser);


                //create roles
                registerUser.Roles.ForEach(role => { tenantService.createTenantRole(register.IdTenant, role.IdRole); });

                if (!registerUser.IsUser)
                {
                    //create db
                    registerUser.Databases.ForEach(role => { tenantService.createTenantDBAccess(register.IdTenant, role.IdDb); });

                }

            }
            catch (Exception e)
            {
                response.responseNumber = 0;
                response.response = e.Message;

                throw new Exception(e.Message);
            }

            response.responseNumber = 1;
            response.response ="Creado Correctamente!";
            return response;
        }
        /// <summary>
        /// create user and tenant space Group
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public GlobalResponse CreateTenantGroup(RegisterUserDTO registerUser)
        {
            GlobalResponse response = new GlobalResponse();

            try
            {
                //map model to okta model
                OktaUser registerOktaUser = imapper.Map<OktaUser>(registerUser);
                registerOktaUser.profile = new Profile();
                registerOktaUser.profile = imapper.Map<Profile>(registerUser);
                Credentials credentials = new Credentials();
                credentials.password = new Password();
                credentials.password = imapper.Map<Password>(registerUser);
                registerOktaUser.credentials = credentials;
                // Create user under a gruop (tenant space )
                string status = iokta.CreateUser(body: registerOktaUser);

                oktaGroup oktaGroup = imapper.Map<oktaGroup>(registerUser);
                string tenant_space_ID  = iokta.CreateGroups(body: oktaGroup);

                //TODO:add db space id 
                //create user 
                 Tenant register= CreateBasicUser(registerUser);
                //create roles
                registerUser.Roles.ForEach(role => { tenantService.createTenantRole(register.IdTenant, role.IdRole); });

                if (!registerUser.IsUser)
                {
                    //create db
                    registerUser.Databases.ForEach(role => { tenantService.createTenantDBAccess(register.IdTenant, role.IdDb); });

                }

            }
            catch (Exception e)
            {
                response.responseNumber = 0;
                response.response = e.Message;

                throw new Exception(e.Message);
            }

            response.responseNumber = 1;
            response.response = "Creado Correctamente!";
            return response;
        }


        private  Tenant CreateBasicUser( RegisterDTO registerUser)
        {
            Tenant register = imapper.Map<Tenant>(registerUser);

            try
            {  //create user 
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
