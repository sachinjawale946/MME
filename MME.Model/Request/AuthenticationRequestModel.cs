using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Request
{
    public class AuthenticationRequestModel
    {
        public AuthenticationRequestModel()
        {
            username = string.Empty;
            password = string.Empty;
        }

        [Required(ErrorMessage = "Username is required field")]
        [StringLength(15, ErrorMessage = "Username max length is 15")]
        [RegularExpression(@"^[^\s\,]+$",ErrorMessage = "Space and Coma are not allowed.")]
        public string username { get; set; }
        [Required(ErrorMessage = "Password is required field")]
        [StringLength(15, ErrorMessage = "Password max length is 15")]
        [RegularExpression(@"^[^\s\,]+$", ErrorMessage = "Space and Coma are not allowed.")]
        public string password { get; set; }
    }
}
