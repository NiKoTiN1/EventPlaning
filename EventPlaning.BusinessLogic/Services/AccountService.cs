using AutoMapper;
using EventPlanning.BusinessLogic.Interfaces;
using EventPlanning.Domain.Enums;
using EventPlanning.Domain.Models;
using EventPlanning.ViewModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EventPlanning.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        public AccountService(
            UserManager<Account> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            ILogger<AccountService> logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _roleManager = roleManager;
        }

        private readonly UserManager<Account> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;

        public async Task<Account> GetByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> UpdateUser(Account user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<Account> GetById(string userId)
        {
            return await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
        }

        public bool VerifyUser(Account user, string password)
        {
            _logger.LogInformation("Method VerifyUser started.");
            if (user == null)
            {
                _logger.LogWarning("Method VerifyUser finished with error.");
                return false;
            }
            _logger.LogInformation("Method VerifyUser finished succeed.");
            return _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success;
        }

        public async Task<bool> AddRoleToUser(Account user, Roles role)
        {
            _logger.LogInformation("Method AddRoleToUser started.");
            IdentityResult result = await _userManager.AddToRoleAsync(user, role.ToString()).ConfigureAwait(false);
            await _userManager.UpdateSecurityStampAsync(user).ConfigureAwait(false);

            _logger.LogInformation("Method AddRoleToUser finished succeed.");
            return result.Succeeded;
        }

        public async Task<Account> CreateUser(RegisterViewModel model)
        {
            _logger.LogInformation("Method CreateUser started.");
            var user = _mapper.Map<Account>(model);
            if (!(await _userManager.CreateAsync(user, model.Password)).Succeeded)
            {
                _logger.LogWarning("Method CreateUser finished with error.");
                return null;
            }
            var roleAdded = await AddRoleToUser(user, Roles.Creator);
            if (!roleAdded)
            {
                _logger.LogWarning("Method CreateUser finished with error.");
                await _userManager.DeleteAsync(user);
                return null;
            }
            _logger.LogInformation("Method CreateUser finished succeed.");
            return user;
        }

        public async Task<bool> IsAdmin(string userId)
        {
            _logger.LogInformation("Method IsAdmin started.");
            var user = await _userManager.FindByIdAsync(userId);

            _logger.LogInformation("Method IsAdmin finished succeed.");
            return await _userManager.IsInRoleAsync(user, Roles.Creator.ToString());
        }
    }
}
