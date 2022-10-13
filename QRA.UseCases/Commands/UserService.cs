
using AutoMapper;
using QRA.Entities.Entities;
using QRA.Entities.Models;
using QRA.Entities.oktaModels;
using QRA.Persistence;
using QRA.UseCases.contracts;
using QRA.UseCases.DTOs;
using System.Text;

namespace QRA.UseCases.commands
{
    public class UserService : IUserService
    {
        private QRAchallengeContext _context;
        private readonly IMapper imapper;
        private readonly ITenantsLoginService tenantsLoginService;
        private readonly ITenantService tenantService;


        public UserService(QRAchallengeContext qR, IMapper mapper, ITenantsLoginService tenantsLogin, ITenantService tenant)
        {
            _context = qR;
            imapper = mapper;
            tenantsLoginService = tenantsLogin;
            tenantService = tenant;
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
        public Tenant CreateAdmin(RegisterDTO registerDTO)
        {
            // Create user in okta
            OktaUser registerOktaUser = imapper.Map<OktaUser>(registerDTO);

           


            //create user in tenants 
            Tenant register = imapper.Map<Tenant>(registerDTO);
            register.IdTenant= tenantService.create(register);

            //create tenantslogin 
            TenantsLogin registerlogin = imapper.Map<TenantsLogin>(registerDTO);
            //create password 
            byte[] hash, salt;
            CreateHashPassword( registerDTO.Password,  out hash, out salt);
            registerlogin.IdTenant = register.IdTenant;
            registerlogin.PasswordEncrypted = hash;
            registerlogin.PasswordSalt = salt;
            //create tennats login
            register.IdTenant = tenantsLoginService.create(registerlogin);

            return register;
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
                //create user 
                Tenant register = imapper.Map<Tenant>(registerUser);
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
