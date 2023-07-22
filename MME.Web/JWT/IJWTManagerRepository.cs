using MME.Model.Shared;

namespace MME.Web.JWT
{
    public interface IJWTManagerRepository
    {
        UserModel Authenticate(UserModel user);
    }
}
