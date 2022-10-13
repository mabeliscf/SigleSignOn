using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string Lastname { get; set; } = null!;

        [Required]
        public string Phone { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public int  Logintype { get; set; }  //1-local 2-other

        public bool IsAdmin { get; set; }

        public bool IsTenant { get; set; }

        public bool IsUser { get; set; }



    }
}
