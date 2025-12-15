
namespace AdvanceCRM.Membership.Pages
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Administration.Repositories;
    using Serenity;
    using Serenity.Abstractions;
    using Serenity.Data;
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using Serenity.Web.Providers;
    using System;
    
    // using System.Web.Security;
    using AdvanceCRM.Common.Calendar;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.DataProtection;
    using Microsoft.Extensions.DependencyInjection;
    using AdvanceCRM.MultiTenancy;

    public partial class AccountController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<AccountController> _logger;
        private readonly IRequestContext Context;
        private readonly IConfiguration _configuration;

        [ActivatorUtilitiesConstructor]
        public AccountController(ISqlConnections connections, IRequestContext context, IMemoryCache cache,
            ITwoLevelCache twoLevelCache,
            ILogger<AccountController> logger, IAuthenticationService authenticationService,
            ITextLocalizer localizer, IConfiguration configuration, IWebHostEnvironment env,
            IUserRetrieveService userRetriever, IDataProtectionProvider dataProtectionProvider,
            SubdomainService subdomainService, IServiceScopeFactory scopeFactory, ITenantAccessor tenantAccessor)
            : this(connections, authenticationService, userRetriever, env, configuration, localizer, dataProtectionProvider, twoLevelCache, subdomainService, scopeFactory, tenantAccessor)
        {
            _cache = cache;
            _logger = logger;
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
        }
        [HttpGet, Route("ChangePassword"), Authorize]
        public ActionResult ChangePassword()
        {
            return View(MVC.Views.Membership.Account.ChangePassword.AccountChangePassword);
        }

        [HttpPost, Route("ChangePassword"), JsonFilter, ServiceAuthorize]
        public Result<ServiceResponse> ChangePassword(ChangePasswordRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return this.InTransaction("Default", uow =>
            {

                if (string.IsNullOrEmpty(request.OldPassword))
                    throw new ArgumentNullException("oldPassword");
                var userDefinition = (UserDefinition)Context.User.ToUserDefinition();
                var username = userDefinition.Username;

                var userDef = _userRetriever.ByUsername(username) as UserDefinition;
                bool isValid = false;
                if (userDef != null)
                {
                    var calcHash = UserRepository.CalculateHash(request.OldPassword, userDef.PasswordSalt);
                    isValid = string.Equals(calcHash, userDef.PasswordHash, StringComparison.OrdinalIgnoreCase);
                }

                if (!isValid)
                    throw new ValidationError("CurrentPasswordMismatch", Localizer.Get("Validation.CurrentPasswordMismatch"));

                if (request.ConfirmPassword != request.NewPassword)
                    throw new ValidationError("PasswordConfirmMismatch", Localizer.Get("Validation.PasswordConfirm"));

                request.NewPassword = UserRepository.ValidatePassword(request.NewPassword, Localizer);

                var salt = Serenity.IO.TemporaryFileHelper.RandomFileCode().Substring(0, 5);
                var hash = SiteMembershipProvider.ComputeSHA512(request.NewPassword + salt);
                var userId = int.Parse(Context.User.GetIdentifier());

                UserRepository.CheckPublicDemo(userId);

                uow.Connection.UpdateById(new UserRow
                {
                    UserId = userId,
                    PasswordSalt = salt,
                    PasswordHash = hash
                });

                Cache.InvalidateOnCommit(uow, UserRow.Fields);

                return new ServiceResponse();
            });
        }
    }
}