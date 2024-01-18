using Management.Web.Models;

namespace Management.Web.Repositories.Abstract
{
    public interface IAccountRepo
    {

        Task<User> VerifyLogin(string username, string password);

        
        
    }
}
