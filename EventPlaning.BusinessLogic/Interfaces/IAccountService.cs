using EventPlanning.Domain.Models;
using EventPlanning.ViewModels.Models;

namespace EventPlanning.BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        Task<Account> GetByEmail(string email);
        Task<Account> GetById(string userId);
        Task<bool> UpdateUser(Account user);
        bool VerifyUser(Account user, string password);
        Task<Account> CreateGuest(RegisterGuestViewModel model);
        Task<bool> IsAdmin(string userId);
    }
}
