using AutoMapper;
using EventPlanning.BusinessLogic.Interfaces;
using EventPlanning.DataAccess.Interfaces;
using EventPlanning.DataAccess.Repositories;
using EventPlanning.Domain.Enums;
using EventPlanning.Domain.Models;
using EventPlanning.ViewModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Web.Http;

namespace EventPlanning.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Account> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;
        private readonly IGuestService _guestService;
        private readonly ICreatorService _creatorService;
        private readonly ITokenService _tokenService;

        public AccountService(
            UserManager<Account> userManager,
            IMapper mapper,
            ILogger<AccountService> logger,
            IGuestService guestService,
            ICreatorService creatorService,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _guestService = guestService;
            _creatorService = creatorService;
            _tokenService = tokenService;
        }

        public async Task<Account> GetByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<Account> GetById(string userId)
        {
            return await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
        }

        public async Task<TokenViewModel> CreateAccount(RegisterModel model)
        {
            var userType = (Roles) Enum.Parse(typeof(Roles), model.UserType, ignoreCase: true);

            Account account;

            if (userType == Roles.Guest)
            {
                var guest = _mapper.Map<Guest>(model);
                account = await CreateGuest(guest, model.Password);
            }
            else
            {
                var creator = _mapper.Map<Creator>(model);
                account = await CreateCreator(creator, model.Password);
            }

            if (account == null)
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Cannot create an account");
                throw new HttpResponseException(response);
            }

            account.RefreshToken = await _tokenService.GenerateRefreshToken();

            var isUpdated = await UpdateUser(account);

            if (!isUpdated)
            {
                return null;
            }

            var tokenModel = _mapper.Map<TokenViewModel>(account.RefreshToken);
            tokenModel.AccessToken = await _tokenService.GenerateToken(account);

            return tokenModel;
        }

        public async Task<bool> UpdateUser(Account user)
        {
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
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

        public async Task<bool> IsAdmin(string userId)
        {
            _logger.LogInformation("Method IsAdmin started.");
            var user = await _userManager.FindByIdAsync(userId);

            _logger.LogInformation("Method IsAdmin finished succeed.");

            return await _userManager.IsInRoleAsync(user, Roles.Creator.ToString());
        }

        private async Task<Account> CreateGuest(Guest guest, string password)
        {
            _logger.LogInformation("Method CreateGuest started.");

            if (!(await _userManager.CreateAsync(guest.Account, password)).Succeeded)
            {
                _logger.LogWarning("Method CreateGuest finished with error.");

                return null;
            }

            await _guestService.CreateGuest(guest);

            var roleAdded = await AddRoleToUser(guest.Account, Roles.Guest);

            if (!roleAdded)
            {
                _logger.LogWarning("Method CreateGuest finished with error.");
                await _userManager.DeleteAsync(guest.Account);

                return null;
            }

            _logger.LogInformation("Method CreateGuest finished succeed.");

            return guest.Account;
        }

        private async Task<Account> CreateCreator(Creator creator, string password)
        {
            _logger.LogInformation("Method CreateCreator started.");

            if (!(await _userManager.CreateAsync(creator.Account, password)).Succeeded)
            {
                _logger.LogWarning("Method CreateCreator finished with error.");

                return null;
            }

            await _creatorService.CreateCreator(creator);

            var roleAdded = await AddRoleToUser(creator.Account, Roles.Creator);

            if (!roleAdded)
            {
                _logger.LogWarning("Method CreateCreator finished with error.");
                await _userManager.DeleteAsync(creator.Account);

                return null;
            }

            _logger.LogInformation("Method CreateCreator finished succeed.");

            return creator.Account;
        }
    }
}
