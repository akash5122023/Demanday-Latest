using AdvanceCRM.Web.Helpers;
using AdvanceCRM.Contacts;
using AdvanceCRM.Template;
using Serenity.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Dapper;
using AdvanceCRM.Administration;
using AdvanceCRM.Common;

namespace AdvanceCRM.Feedback
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackDetailsController : ControllerBase
    {
        private readonly ISqlConnections _connections;

        public FeedbackDetailsController(ISqlConnections connections)
        {
            _connections = connections;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();  // or return your view model
        }

        [HttpPost("ValidateAndGenerateOTP")]
        public async Task<IActionResult> ValidateAndGenerateOTP([FromForm] string phone)
        {
            using var cn = _connections.NewFor<ContactsRow>();
            cn.Open();
            var contact = await cn.QueryFirstOrDefaultAsync<ContactsRow>(
                "SELECT TOP 1 * FROM Contacts WHERE Phone = @phone",
                new { phone });

            if (contact == null)
                return BadRequest("This phone number is not registered with us");

            var otp = new Random().Next(10000, 99999);
            var now = DateTime.Now;

            try
            {
                // Insert into OPTLog
                using var logCn = _connections.NewFor<OptLogRow>();
                logCn.Open();
                await logCn.ExecuteAsync(
                    "INSERT INTO OPTLog(Phone,OPT,Validity) VALUES(@phone,@otp,@validity)",
                    new { phone, otp, validity = now });

                // Lookup SMS template
                using var tplCn = _connections.NewFor<OtherTemplatesRow>();
                tplCn.Open();
                var template = await tplCn.QueryFirstOrDefaultAsync<OtherTemplatesRow>(
                    "SELECT TOP 1 OTPSMSTemplate AS OtpsmsTemplate, OTPSMSTemplateID AS OtpsmsTemplateId FROM OtherTemplates");

                var smsResult = SendCustomSMS(phone, otp.ToString(), template.OtpsmsTemplateId, template.OtpsmsTemplate);
                if (!smsResult.Contains("SMS"))
                    return BadRequest("Unable to send OTP via SMS");
            }
            catch
            {
                return BadRequest("Unable to send OTP via SMS");
            }

            return Ok(contact.Name);
        }

        [HttpPost("Validated")]
        public async Task<IActionResult> Validated([FromForm] string otp)
        {
            using var cn = _connections.NewFor<OptLogRow>();
            cn.Open();
            var entry = await cn.QueryFirstOrDefaultAsync<OptLogRow>(
                "SELECT TOP 1 * FROM OPTLog WHERE OPT = @otp",
                new { otp });

            if (entry == null)
                return BadRequest("Invalid OTP");

            if (DateTime.Now.Subtract(entry.Validity.Value).TotalMinutes > 15)
                return BadRequest("OTP Expired");

            // Clean up old entries
            using var delCn = _connections.NewFor<OptLogRow>();
            delCn.Open();
            await delCn.ExecuteAsync(
                "DELETE FROM OPTLog WHERE Validity < @cutoff",
                new { cutoff = DateTime.Now.AddDays(-1) });

            return Ok("Valid OTP");
        }

        [HttpGet("Feedback")]
        public IActionResult FeedbackForm()
        {
            // return your Razor view here if needed,
            // or just a blank model for an SPA.
            return Ok(new ModelFeedback());
        }

        [HttpPost("Feedback")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitFeedback([FromForm] ModelFeedback model)
        {
            try
            {
                using var cn = _connections.NewFor<OptLogRow>();
                cn.Open();
                await cn.ExecuteAsync(
                    @"INSERT INTO FeedbackDetails(Phone,Name,Refer,Details,Service)
                      VALUES(@Phone,@Name,0,@Details,@Service)",
                    new { model.Phone, model.Name, model.Details, model.Service });

                // Optionally get the last inserted ID
                using var lastCn = _connections.NewFor<FeedbackDetailsRow>();
                lastCn.Open();
                var last = await lastCn.QueryFirstOrDefaultAsync<FeedbackDetailsRow>(
                    "SELECT TOP 1 Id FROM FeedbackDetails ORDER BY Id DESC");

                // Send feedback SMS
                using var tplCn = _connections.NewFor<OtherTemplatesRow>();
                tplCn.Open();
                var template = await tplCn.QueryFirstOrDefaultAsync<OtherTemplatesRow>(
                    "SELECT TOP 1 FeedbackSMSTemplate AS FeedbackSmsTemplate, FeedbackSMSTemplateID AS FeedbackSmsTemplateId FROM OtherTemplates");

                SendCustomSMS(model.Phone, "", template.FeedbackSmsTemplateId, template.FeedbackSmsTemplate);

                return Ok("Your feedback is registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string SendCustomSMS(string phone, string otp, string templateId, string templateText)
        {
            var msg = string.IsNullOrEmpty(otp)
                ? "Your feedback is registered successfully"
                : $"OTP for complaint registration is: {otp}";

            try
            {
                return SMSHelper.SendSMS(phone, msg, templateId);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
