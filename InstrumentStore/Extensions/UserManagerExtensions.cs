using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InstrumentStore.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<string> GetUserIdByNameAsync(this UserManager<ApplicationUser> userManager, string name)
        {
            var user = await userManager.FindByNameAsync(name);
            return user.Id;
        }
    }
}
