using Management.Web.Models;
using Management.Web.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Management.Web.Repositories.Concrete
{
    public class AccountRepo : IAccountRepo
    {
        private readonly AppDbContext dbContext;

        public AccountRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> VerifyLogin(string username,string password)
        {
            
            return await dbContext.Users.FirstOrDefaultAsync(x => x.Username == username &&  x.Password==password);

            
        }
    }
}
