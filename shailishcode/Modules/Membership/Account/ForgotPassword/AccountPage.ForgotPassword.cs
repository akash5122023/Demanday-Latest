
namespace AdvanceCRM.Membership.Pages
{
    using Administration;
    using Serenity;
    using Serenity.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.IO;
    

    public partial class AccountController : Controller
    {
        [HttpGet, Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            if (UseAdminLTELoginBox)
                return View(MVC.Views.Membership.Account.ForgotPassword.AccountForgotPassword_AdminLTE);
            else
                return View(MVC.Views.Membership.Account.ForgotPassword.AccountForgotPassword);
        }

        [HttpPost, JsonFilter, Route("ForgotPassword")]
        public Result<ServiceResponse> ForgotPassword(ForgotPasswordRequest request,
            [FromServices] IOptions<EnvironmentSettings> options = null)
        {
            return this.UseConnection("Default", connection =>
            {
                if (request is null)
                    throw new ArgumentNullException(nameof(request));

                if (string.IsNullOrEmpty(request.Email))
                    throw new ArgumentNullException("email");

                var user = connection.TryFirst<UserRow>(UserRow.Fields.Email == request.Email);
                if (user == null)
                    throw new ValidationError("CantFindUserWithEmail", Texts.Validation.CantFindUserWithEmail.ToString(Localizer));

                byte[] bytes;
                using (var ms = new MemoryStream())
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(DateTime.UtcNow.AddHours(3).ToBinary());
                    bw.Write(user.UserId.Value);
                    bw.Flush();
                    bytes = ms.ToArray();
                }

                var protector = _dataProtectionProvider.CreateProtector("ResetPassword");
                var token = Convert.ToBase64String(protector.Protect(bytes));

                var externalUrl = options?.Value.SiteExternalUrl ??
                    Request.GetBaseUri().ToString();

                var resetLink = UriHelper.Combine(externalUrl, "Account/ResetPassword?t=");
                resetLink = resetLink + Uri.EscapeDataString(token);

                var emailModel = new ResetPasswordEmailModel();
                emailModel.Username = user.Username;
                emailModel.DisplayName = user.DisplayName;
                emailModel.ResetLink = resetLink;

                var emailSubject = Texts.Forms.Membership.ResetPassword.EmailSubject.ToString();
                var emailBody = TemplateHelper.RenderViewToString(HttpContext.RequestServices,
                    MVC.Views.Membership.Account.ResetPassword.AccountResetPasswordEmail, emailModel);

                Common.EmailHelper.SendSystem(emailSubject, emailBody, user.Email);

                return new ServiceResponse();
            });
        }
    }
}
