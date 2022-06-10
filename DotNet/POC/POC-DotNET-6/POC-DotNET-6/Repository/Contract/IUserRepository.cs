using POC_DotNET_6.Models;

namespace POC_DotNET_6.Repository.Contract
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        User Authenticate(string username, string password);
        User Register(string username, string password);
    }
}
