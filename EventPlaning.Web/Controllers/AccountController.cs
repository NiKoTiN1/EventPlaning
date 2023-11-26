using AutoMapper;
using EventPlanning.BusinessLogic.Interfaces;
using EventPlanning.ViewModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanning.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        public AccountController(IAccountService accountService,
            ILogger<AccountController> logger,
            ITokenService tokenService,
            IMapper mapper)
        {
            _accountService = accountService;
            _logger = logger;
            _tokenService = tokenService;
            this.mapper = mapper;
        }

        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        private readonly ITokenService _tokenService;
        private readonly IMapper mapper;

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            _logger.LogInformation("Method Register started.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Method Register finished with BadRequest.");

                return BadRequest("Model error!");
            }

            var tokenModel = await _accountService.CreateAccount(model);

            _logger.LogInformation("Method Register finished with Ok.");

            return Ok(tokenModel);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            _logger.LogInformation("Method Login started.");

            var user = await _accountService.GetByEmail(model.Email);

            if (!_accountService.VerifyUser(user, model.Password))
            {
                _logger.LogWarning("Method Login finished with BadRequest.");

                return BadRequest("Invalid email or password.");
            }

            user.RefreshToken = await _tokenService.GenerateRefreshToken();

            var isUpdated = await _accountService.UpdateUser(user);

            if (!isUpdated)
            {
                _logger.LogWarning("Method Login finished with BadRequest.");

                _tokenService.RemoveToken(user.RefreshToken.Token);

                return BadRequest("User cannot set refresh error!");
            }

            TokenViewModel tokenModel = mapper.Map<TokenViewModel>(user.RefreshToken);
            tokenModel.AccessToken = await _tokenService.GenerateToken(user);

            _logger.LogInformation("Method Login finished with Ok.");

            return Ok(tokenModel);
        }
    }
}
