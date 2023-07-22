using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Shared
{
    [Table("tblUsers")]
    public class UserModel
    {
        [Key]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string InviteCode { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string ProfilePic { get; set; }
        public string Society { get; set; }
        public string Area { get; set; }
        public string Location { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public string Latitude { get; set; }

        public string Longtitude { get; set; }
        [ForeignKey("State")]
        public int StateId { get; set; }
        [ForeignKey("Pincode")]
        public int PincodeId { get; set; }
        [ForeignKey("Language")]
        public int LanguageId { get; set; }
        [ForeignKey("Religion")]
        public int ReligionId { get; set; }
        [ForeignKey("Caste")]
        public int CasteId { get; set; }
        [ForeignKey("SubCaste")]
        public int SubCasteId { get; set; }
        [ForeignKey("Occupation")]
        public int OccupationId { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public bool IsActive { get; set; }

        public int State { get; set; }
        public int Pincode { get; set; }
        public int Language { get; set; }
        public int Religion { get; set; }
        public int Caste { get; set; }
        public int SubCaste { get; set; }
        public int Occupation { get; set; }
        public int Role { get; set; }

    }
}
