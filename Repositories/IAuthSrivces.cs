using ManageEmployeApp.Modle;

namespace ManageEmployeApp.Repositories
{
    public interface IAuthSrivces
    {
     string GenerateToken(string user);
    }
}
