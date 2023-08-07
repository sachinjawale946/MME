using ImageMagick;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MME.Data;
using MME.Model.Lookups;
using MME.Model.Request;
using MME.Model.Response;
using MME.Model.Shared;
using MME.Web.Filters;
using MME.Web.JWT;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MME.Web.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(MMEExceptionFilter))]
    public class UsersApiController : ControllerBase
    {
        readonly IJWTManagerRepository _jWTManagerRepository;
        readonly MMEAppDBContext _context;
        readonly IConfiguration _iconfiguration;

        public UsersApiController(IJWTManagerRepository jWTManagerRepository, MMEAppDBContext context, IConfiguration iconfiguration)
        {
            _jWTManagerRepository = jWTManagerRepository;
            _context = context;
            _iconfiguration = iconfiguration;
        }

        [AllowAnonymous]
        [HttpPost, Route("~/api/v1/authenticate-user")]
        public AuthenticationResponseModel Post(AuthenticationRequestModel model)
        {
            var token = _jWTManagerRepository.Authenticate(model);
            if (token == null)
            {
                return new AuthenticationResponseModel();
            }
            return token;
        }

        [Authorize(Policy = "MMEJwtScheme")]
        [HttpGet, Route("~/api/v1/members-getprofilepicture/{userid}/{Gender}")]
        public string ProfilePicture(Guid userid, string Gender)
        {
            var profilesFolderPath = _iconfiguration["allprofileimages"].ToString();
            var defaultBoyImage = _iconfiguration["defaultboyimage"].ToString();
            var defaultGirlImage = _iconfiguration["defaultgirlimage"].ToString();
            if (userid != Guid.Empty && !string.IsNullOrEmpty(Gender))
            {
                var profile = _context.Users.Where(u => u.UserId == userid).FirstOrDefault();
                if (profile != null && !string.IsNullOrEmpty(profile.ProfilePic))
                    return profilesFolderPath + profile.ProfilePic;
                else
                {
                    if (Gender == "Male")
                        return defaultBoyImage;
                    else
                        return defaultGirlImage;
                }
            }
            return defaultBoyImage;
        }

        [Authorize(Policy = "MMEJwtScheme")]
        [HttpPost, Route("~/api/v1/members-removeprofilepicture")]
        public string RemoveProfilePicture(ProfilePictureRequestModel model)
        {
            if (model != null && model.userid != Guid.Empty)
            {
                var user = _context.Users.Where(u => u.UserId == model.userid).FirstOrDefault();
                if (user != null)
                {
                    user.ProfilePic = null;
                    _context.Users.Update(user);
                    _context.SaveChanges();
                    return Api_Result_Lookup.Success;
                }
                return Api_Result_Lookup.Error;
            }
            return Api_Result_Lookup.Error;
        }

        [Authorize(Policy = "MMEJwtScheme")]
        [HttpPost, Route("~/api/v1/members-saveprofilepicture")]
        public string AddProfilePicture(ProfilePictureRequestModel model)
        {
            if (model != null && model.userid != Guid.Empty && model.picture != null && model.picture.Length > 0 && !string.IsNullOrEmpty(model.pictureextenstion))
            {
                var profilesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + _iconfiguration["profilepics"].ToString());
                var profilesThumbesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + _iconfiguration["profilepicthumbs"].ToString());
                var profile = _context.Users.Where(u => u.UserId == model.userid).FirstOrDefault();
                if (profile != null)
                {
                    var ProfilePic = Guid.NewGuid() + model.pictureextenstion;
                    // main profile pic
                    System.IO.File.WriteAllBytes(Path.Combine(profilesFolderPath, ProfilePic), model.picture);
                    profile.ProfilePic = ProfilePic;
                    _context.Users.Update(profile);
                    _context.SaveChanges();
                    return Api_Result_Lookup.Success;
                }
                return Api_Result_Lookup.Error;
            }
            return Api_Result_Lookup.Error;
        }

        [Authorize(Policy = "MMEJwtScheme")]
        [HttpPost, Route("~/api/v1/members-add-fcmtoken")]
        public string AddFCMToken(FCMRequestModel model)
        {
            if (model != null && !string.IsNullOrEmpty(model.token))
            {
                var profile = _context.Users.Where(u => u.UserId == model.userid).FirstOrDefault();
                if (profile != null)
                {
                    profile.FCMToken = model.token;
                    _context.Users.Update(profile);
                    _context.SaveChanges();
                    return Api_Result_Lookup.Success;
                }
                return Api_Result_Lookup.Error;
            }
            return Api_Result_Lookup.Error;
        }

        [Authorize(Policy = "MMEJwtScheme")]
        [HttpGet, Route("~/api/v1/members-getprofile/{userid}")]
        public ProfileResponseModel Profile(Guid userid)
        {
            if (userid != Guid.Empty)
            {
                var profilesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + _iconfiguration["profilepics"].ToString());
                var profile = _context.Users.Where(u => u.UserId.ToString().ToLower() == userid.ToString().ToLower()).FirstOrDefault();
                if (profile != null)
                {
                    return new ProfileResponseModel
                    {
                        profilepic = (string.IsNullOrEmpty(profile.ProfilePic)) ? null : System.IO.File.ReadAllBytes(Path.Combine(profilesFolderPath, profile.ProfilePic)),
                        Area = profile.Area,
                        BirthDate = Convert.ToDateTime(profile.BirthDate),
                        CasteId = profile.CasteId,
                        City = profile.City,
                        Email = profile.Email,
                        FirstName = profile.FirstName,
                        Gender = profile.Gender,
                        InviteCode = profile.InviteCode,
                        IsActive = profile.IsActive,
                        Landmark = profile.Landmark,
                        LanguageId = profile.LanguageId,
                        LastName = profile.LastName,
                        Latitude = profile.Latitude,
                        Location = profile.Location,
                        Longtitude = profile.Longtitude,
                        MaritalStatus = profile.MaritalStatus,
                        MiddleName = profile.MiddleName,
                        Mobile = profile.Mobile,
                        OccupationId = profile.OccupationId,
                        PincodeId = profile.PincodeId,
                        ReligionId = profile.ReligionId,
                        RoleId = profile.RoleId,
                        Society = profile.Society,
                        StateId = profile.StateId,
                        SubCasteId = profile.SubCasteId,
                        UserId = profile.UserId,
                        Username = profile.Username,
                    };
                }
            }
            return new ProfileResponseModel();
        }

        [Authorize(Policy = "MMEJwtScheme")]
        [HttpPost, Route("~/api/v1/members-search")]
        public MemberResponseWrapperModel Search(MemberRequestModel model)
        {
            var profilesPicsFolderPath = _iconfiguration["allprofileimages"].ToString();

            if (model.page == 0) model.page = 1;
            if (model.pagesize == 0) model.pagesize = Convert.ToInt16(_iconfiguration["memberpagesize"].ToString());

            if (!string.IsNullOrEmpty(model.membername))
            {
                return new MemberResponseWrapperModel
                {
                    MembersCount = _context.Users.Where(c => c.IsActive && (c.FirstName.ToLower().Contains(model.membername.ToLower())
                            || c.LastName.ToLower().Contains(model.membername.ToLower()))).Count(),

                    Members = _context.Users.Where(c => c.IsActive && (c.FirstName.ToLower().Contains(model.membername.ToLower())
                            || c.LastName.ToLower().Contains(model.membername.ToLower())))
                        .Include("Occupation")
                        .OrderBy(c => c.FirstName).ThenBy(c => c.LastName)
                        .Skip((model.page - 1) * model.pagesize)
                        .Take(model.pagesize)
                        .Select(o => new MemberResponseModel
                        {
                            firstname = o.FirstName,
                            lastname = o.LastName,
                            mobile = o.Mobile,
                            userid = o.UserId,
                            username = o.Username,
                            fullname = o.FirstName + " " + o.LastName,
                            occupation = (o.Occupation == null) ? string.Empty : o.Occupation.Occupation,
                            gender = o.Gender,
                            fulladdress = o.City,
                            profilepicurl = (string.IsNullOrEmpty(o.ProfilePic)) ? string.Empty : profilesPicsFolderPath + o.ProfilePic,
                        }).ToList()
                };
            }
            else
            {
                return new MemberResponseWrapperModel
                {
                    MembersCount = _context.Users.Where(c => c.IsActive)
                       .OrderBy(c => c.FirstName).ThenBy(c => c.LastName).Count(),

                    Members = _context.Users.Where(c => c.IsActive)
                       .OrderBy(c => c.FirstName).ThenBy(c => c.LastName)
                       .Skip((model.page - 1) * model.pagesize)
                       .Take(model.pagesize)
                       .Select(o => new MemberResponseModel
                       {
                           firstname = o.FirstName,
                           lastname = o.LastName,
                           mobile = o.Mobile,
                           userid = o.UserId,
                           username = o.Username,
                           fullname = o.FirstName + " " + o.LastName,
                           occupation = (o.Occupation == null) ? string.Empty : o.Occupation.Occupation,
                           gender = o.Gender,
                           fulladdress = o.City,
                           profilepicurl = (string.IsNullOrEmpty(o.ProfilePic)) ? string.Empty : profilesPicsFolderPath + o.ProfilePic,
                       }).ToList()
                };
            }
        }

    }
}
