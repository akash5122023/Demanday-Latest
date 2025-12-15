namespace AdvanceCRM.Membership.Pages
{
    using Administration;
    using Administration.Repositories;
    using Serenity;
    using Serenity.Data;
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using Serenity.Localization;
    using Microsoft.AspNetCore.DataProtection;
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using Serenity.Web.Providers;

    public partial class AccountController : Controller
    {
        [HttpGet, Route("ResetPassword")]
        public ActionResult ResetPassword(string t)
        {
            int userId;
            try
            {
                var protector = _dataProtectionProvider.CreateProtector("ResetPassword");
                var bytes = protector.Unprotect(Convert.FromBase64String(t));
                using var ms = new MemoryStream(bytes);
                using var br = new BinaryReader(ms);
                var dt = DateTime.FromBinary(br.ReadInt64());
                if (dt < DateTime.UtcNow)
                    return Error(Texts.Validation.InvalidResetToken.ToString(Localizer));

                userId = br.ReadInt32();
            }
            catch (Exception)
            {
                return Error(Texts.Validation.InvalidResetToken.ToString(Localizer));
            }

            using (var connection = _connections.NewFor<UserRow>())
            {
                var user = connection.TryById<UserRow>(userId);
                if (user == null)
                    return Error(Texts.Validation.InvalidResetToken.ToString(Localizer));
            }

            if (UseAdminLTELoginBox)
                return View(MVC.Views.Membership.Account.ResetPassword.AccountResetPassword_AdminLTE, new ResetPasswordModel { Token = t });
            else
                return View(MVC.Views.Membership.Account.ResetPassword.AccountResetPassword, new ResetPasswordModel { Token = t });
        }

        [HttpPost, Route("ResetPassword"), JsonFilter]
        public Result<ServiceResponse> ResetPassword(ResetPasswordRequest request)
        {
            return this.InTransaction("Default", uow =>
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));

                if (string.IsNullOrEmpty(request.Token))
                    throw new ArgumentNullException("token");

                int userId;
                var protector = _dataProtectionProvider.CreateProtector("ResetPassword");
                var bytes = protector.Unprotect(Convert.FromBase64String(request.Token));
                using (var ms = new MemoryStream(bytes))
                using (var br = new BinaryReader(ms))
                {
                    var dt = DateTime.FromBinary(br.ReadInt64());
                    if (dt < DateTime.UtcNow)
                        throw new ValidationError(Texts.Validation.InvalidResetToken.ToString(Localizer));

                    userId = br.ReadInt32();
                }

                UserRow user;
                using (var connection = _connections.NewFor<UserRow>())
                {
                    user = connection.TryById<UserRow>(userId);
                    if (user == null)
                        throw new ValidationError(Texts.Validation.InvalidResetToken.ToString(Localizer));
                }

                if (request.ConfirmPassword != request.NewPassword)
                    throw new ValidationError("PasswordConfirmMismatch", Localizer.Get("Validation.PasswordConfirm"));

                request.NewPassword = UserRepository.ValidatePassword(request.NewPassword, Localizer);

                var salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
                var hash = SiteMembershipProvider.ComputeSHA512(request.NewPassword + salt);
                UserRepository.CheckPublicDemo(user.UserId);

                uow.Connection.UpdateById(new UserRow
                {
                    UserId = user.UserId.Value,
                    PasswordSalt = salt,
                    PasswordHash = hash
                });

                return new ServiceResponse();
            });
        }
    }
}
