using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MME.Model.Response
{
    public class ProfileResponseModel : ModelBase
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string? InviteCode { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Mobile { get; set; }
        public string? Email { get; set; }
        public string Gender { get; set; }
        public string? MaritalStatus { get; set; }

        private string _profilepic;
        public string profilepic
        {
            get
            {
                return _profilepic;
            }
            set
            {
                _profilepic = value;
                OnPropertyChanged(nameof(profilepic));
            }
        }

        public string? Society { get; set; }
        public string? Area { get; set; }
        public string? Location { get; set; }
        public string? Landmark { get; set; }
        public string? City { get; set; }
        public string? Latitude { get; set; }
        public string? Longtitude { get; set; }

        public int? StateId { get; set; }
        public int? PincodeId { get; set; }
        public int? LanguageId { get; set; }
        public int? ReligionId { get; set; }
        public int? CasteId { get; set; }
        public int? SubCasteId { get; set; }
        public int? OccupationId { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
