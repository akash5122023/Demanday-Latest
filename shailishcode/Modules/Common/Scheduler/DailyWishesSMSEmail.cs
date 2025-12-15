using AdvanceCRM.Administration;
using AdvanceCRM.Common;
using AdvanceCRM.Contacts;
using AdvanceCRM.Enquiry;
using AdvanceCRM.Quotation;
using AdvanceCRM.Sales;
using AdvanceCRM.Services;
using AdvanceCRM.Template;
using Quartz;
using Serenity;
using Serenity.Data;
using Serenity.Abstractions;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Mail;
using Serenity.Extensions.DependencyInjection;

namespace AdvanceCRM.Common.Scheduler
{
    public class DailyWishesSMSEmail : IJob
    {
        public System.Threading.Tasks.Task Execute(IJobExecutionContext context)
        {
            var model = new DashboardPageModel();
            var connection = Dependency.Resolve<ISqlConnections>().NewByKey("Default");
            var cmp = CompanyDetailsRow.Fields;


            var Company = connection.TryById<CompanyDetailsRow>(1, cp => cp
                .SelectTableFields()
                .Select(cmp.Name)
                .Select(cmp.Address)
                .Select(cmp.AutoSMSAppointments)
                );


            if (Company.AutoSMSAppointments.HasValue && Company.AutoSMSAppointments.Value == true)
            {
                //Daily Appointment SMS check
                var dsl = AppointmentSmsLogRow.Fields;
                var lastDate1 = connection.TryFirst<AppointmentSmsLogRow>(ts => ts
                 .SelectTableFields()
                 .Select(dsl.Date)
                 .Where(dsl.Date == DateTime.Now.Date)
                 );

                if (lastDate1 == null)
                {
                    var ast = AppointmentTemplateRow.Fields;
                    var AppTmpt = connection.TryFirst<AppointmentTemplateRow>( qq => qq
                         .SelectTableFields()
                         .Select(ast.MondaySMS)
                         .Select(ast.TuesdaySMS)
                         .Select(ast.WednesdaySMS)
                         .Select(ast.ThursdaySMS)
                         .Select(ast.FridaySMS)
                        .Select(ast.SaturdaySMS)
                        .Select(ast.SundaySMS)
                    );

                    var e = EnquiryAppointmentsRow.Fields;
                    model.EnquiryAppointmentList = connection.List<EnquiryAppointmentsRow>(aq => aq
                        .SelectTableFields()
                        .Select(e.ContactName)
                        .Select(e.ContactPhone)
                        .Where(e.Status == 1)
                        .Where(new Criteria("CAST(AppointmentDate as DATE) > " + DateTime.Now.ToSqlDate()))
                    );

                    var q = QuotationAppointmentsRow.Fields;
                    model.QuotationAppointmentList = connection.List<QuotationAppointmentsRow>(aq => aq
                        .SelectTableFields()
                        .Select(q.ContactName)
                        .Select(q.ContactPhone)
                        .Where(q.Status == 1)
                        .Where(new Criteria("CAST(AppointmentDate as DATE) > " + DateTime.Now.ToSqlDate()))
                    );

                    var t = TeleCallingAppointmentsRow.Fields;
                    model.TeleAppointmentList = connection.List<TeleCallingAppointmentsRow>(aq => aq
                        .SelectTableFields()
                        .Select(t.ContactName)
                        .Select(t.ContactPhone)
                        .Where(t.Status == 1)
                        .Where(new Criteria("CAST(AppointmentDate as DATE) > " + DateTime.Now.ToSqlDate()))
                    );

                    var i = InvoiceAppointmentsRow.Fields;
                    model.InvoiceAppointmentList = connection.List<InvoiceAppointmentsRow>(aq => aq
                        .SelectTableFields()
                        .Select(i.ContactName)
                        .Select(i.ContactPhone)
                        .Where(i.Status == 1)
                        .Where(new Criteria("CAST(AppointmentDate as DATE) > " + DateTime.Now.ToSqlDate()))
                    );

                    var a = AMCVisitPlannerRow.Fields;
                    model.AMCAppointmentList = connection.List<AMCVisitPlannerRow>(aq => aq
                        .SelectTableFields()
                        .Select(a.ContactName)
                        .Select(a.ContactPhone)
                        .Where(a.Status == 1)
                        .Where(new Criteria("CAST(AppointmentDate as DATE) > " + DateTime.Now.ToSqlDate()))
                    );

                    string msgs = "";
                    string tempid = "";

                    if (DateTime.Now.DayOfWeek.ToString() == "Monday")
                    {
                        msgs = AppTmpt.MondaySMS;
                        tempid = AppTmpt.MonTempId;
                    }
                    else if (DateTime.Now.DayOfWeek.ToString() == "Tuesday")
                    {
                        msgs = AppTmpt.TuesdaySMS;
                        tempid = AppTmpt.TueTempId;
                    }
                    else if (DateTime.Now.DayOfWeek.ToString() == "Wednesday")
                    {
                        msgs = AppTmpt.WednesdaySMS;
                        tempid = AppTmpt.WedTempId;
                    }
                    else if (DateTime.Now.DayOfWeek.ToString() == "Thursday")
                    {
                        msgs = AppTmpt.ThursdaySMS;
                        tempid = AppTmpt.ThurTempId;
                    }
                    else if (DateTime.Now.DayOfWeek.ToString() == "Friday")
                    {
                        msgs = AppTmpt.FridaySMS;
                        tempid = AppTmpt.FriTempId;
                    }
                    else if (DateTime.Now.DayOfWeek.ToString() == "Saturday")
                    {
                        msgs = AppTmpt.SaturdaySMS;
                        tempid = AppTmpt.SatTempId;
                    }
                    else if (DateTime.Now.DayOfWeek.ToString() == "Sunday")
                    {
                        msgs = AppTmpt.SundaySMS;
                        tempid = AppTmpt.SunTempId;
                    }

                    int smsCount = 0;

                    if (msgs.IsNullOrEmpty() == false)
                    {
                        foreach (var item in model.EnquiryAppointmentList)
                        {
                            string msg = msgs;
                            string tempId = tempid;

                            msg.Replace("#customername", item.ContactName);

                            var result = SMSHelper.SendSMS(item.ContactPhone, msg, tempId);

                            if (result.Contains("SMS"))
                            {
                                smsCount = smsCount + 1;
                            }
                        }
                        foreach (var item in model.QuotationAppointmentList)
                        {
                            string msg = msgs;
                            string tempId = tempid;

                            msg.Replace("#customername", item.ContactName);

                            var result = SMSHelper.SendSMS(item.ContactPhone, msg, tempId);

                            if (result.Contains("SMS"))
                            {
                                smsCount = smsCount + 1;
                            }
                        }

                        foreach (var item in model.TeleAppointmentList)
                        {
                            string msg = msgs;
                            string tempId = tempid;

                            msg.Replace("#customername", item.ContactName);

                            var result = SMSHelper.SendSMS(item.ContactPhone, msg,tempId);

                            if (result.Contains("SMS"))
                            {
                                smsCount = smsCount + 1;
                            }
                        }

                        foreach (var item in model.InvoiceAppointmentList)
                        {
                            string msg = msgs; string tempId = tempid;

                            msg.Replace("#customername", item.ContactName);

                            var result = SMSHelper.SendSMS(item.ContactPhone, msg,tempId);

                            if (result.Contains("SMS"))
                            {
                                smsCount = smsCount + 1;
                            }
                        }

                        foreach (var item in model.AMCAppointmentList)
                        {
                            string msg = msgs;
                            string tempId = tempid;

                            msg.Replace("#customername", item.ContactName);

                            var result = SMSHelper.SendSMS(item.ContactPhone, msg,tempId);

                            if (result.Contains("SMS"))
                            {
                                smsCount = smsCount + 1;
                            }
                        }

                        var dbSqlStr = "INSERT INTO AppointmentSMSLog(Date,Log) VALUES('" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "','SMS Count: " + Convert.ToString(smsCount) + "')";
                        //Saving to log
                        connection.Execute(dbSqlStr);
                    }
                    else
                    {
                        var dbSqlStr = "INSERT INTO AppointmentSMSLog(Date,Log) VALUES('" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "','SMS template not set, please set in the Appointment Tempate module')";
                        //Saving to log
                        connection.Execute(dbSqlStr);
                    }
                }
            }


            //Daily Wishes check
            var dwl = DailyWishesLogRow.Fields;

            var c = ContactsRow.Fields;
            var sc = SubContactsRow.Fields;

            var ContactListOfWishes = connection.List<ContactsRow>(cw => cw
            .SelectTableFields()
            .Select(c.Name)
            .Select(c.Phone)
            .Select(c.Email)
            .Select(c.Birthdate)
            .Select(c.MarriageAnniversary)
            .Select(c.DateOfIncorporation)
            .Where(new Criteria("CAST(Birthdate as DATE)=" + DateTime.Now.ToSqlDate() +
            " OR CAST(MarriageAnniversary as DATE)=" + DateTime.Now.ToSqlDate() +
            " OR CAST(DateOfIncorporation as DATE)=" + DateTime.Now.ToSqlDate()))
            );

            var SubContactListOfWishes = connection.List<SubContactsRow>(scw => scw
            .SelectTableFields()
            .Select(sc.Name)
            .Select(sc.Phone)
            .Select(sc.Email)
            .Select(sc.Birthdate)
            .Select(sc.MarriageAnniversary)
            .Where(new Criteria("CAST(Birthdate as DATE)=" + DateTime.Now.ToSqlDate() +
            " OR CAST(MarriageAnniversary as DATE)=" + DateTime.Now.ToSqlDate()))
            );

            var dwtr = Template.DailyWishesTemplateRow.Fields;
            var Messages = connection.TryFirst<DailyWishesTemplateRow>(dw => dw
                   .Select(dwtr.BirthdaySMS)
                   .Select(dwtr.BirthTempId)
                   .Select(dwtr.DofTempId)
                   .Select(dwtr.MarriageTempId)
                    .Select(dwtr.MarriageSMS)
                    .Select(dwtr.DofAnniversarySMS)
                    .Select(dwtr.From)
                    .Select(dwtr.Subject)
                    .Select(dwtr.BirthdayEmail)
                    .Select(dwtr.MarriageEmail)
                    .Select(dwtr.DofAnniversaryEmail)
                );

            var bdayMsg = Messages.BirthdaySMS;
            var bdtempid = Messages.BirthTempId;
            var dotempid = Messages.DofTempId;
            var martempid = Messages.MarriageTempId;
            var aniMsg = Messages.MarriageSMS;
            var dtofinCMSg = Messages.DofAnniversarySMS;
            int cntbdaywishes = 0;
            int cntmrgwishes = 0;
            int cntdtofincwishes = 0;

            var u = UserRow.Fields;

            var admin = connection.TryById<UserRow>(1, q => q
                     .SelectTableFields()
                     .Select(u.EmailId)
                     .Select(u.EmailPassword)
                     .Select(u.Host)
                     .Select(u.Port)
                     .Select(u.SSL)
                     );

            //Contacts wishes
            foreach (var item in (ContactListOfWishes))
            {
                try
                {
                    if (item.Birthdate.HasValue)
                    {
                        if (item.Birthdate.Value.ToString("dd-MM") == DateTime.Now.ToString("dd-MM"))
                        {
                            var t_bdayMsg = bdayMsg.Replace("#customername", item.Name);
                           
                            var result = SMSHelper.SendSMS(item.Phone, t_bdayMsg,bdtempid);

                            if (result.Contains("SMS"))
                            {
                                cntbdaywishes = cntbdaywishes + 1;
                            }

                            if (!item.Email.IsNullOrEmpty())
                            {
                                using (var message = new MailMessage(admin.EmailId, item.Email))
                                {

                                    message.Subject = Messages.Subject;
                                    message.Sender = new MailAddress(admin.EmailId, Messages.From); ;
                                    //message.Body = "Scheduled at " + DateTime.Now; //Message body
                                    message.IsBodyHtml = true;
                                    message.Body = Messages.BirthdayEmail; //Message body
                                    EmailHelper.Send(message, admin.EmailId, admin.EmailPassword, (Boolean)admin.SSL, admin.Host, admin.Port.Value);
                                }
                            }
                        }
                    }

                    if (item.MarriageAnniversary.HasValue)
                    {
                        if (item.MarriageAnniversary.Value.ToString("dd-MM") == DateTime.Now.ToString("dd-MM"))
                        {
                            var t_aniMsg = aniMsg.Replace("#customername", item.Name);
                            var result = SMSHelper.SendSMS(item.Phone, t_aniMsg,martempid);

                            if (result.Contains("SMS"))
                            {
                                cntmrgwishes = cntmrgwishes + 1;
                            }

                            if (!item.Email.IsNullOrEmpty())
                            {
                                using (var message = new MailMessage(admin.EmailId, item.Email))
                                {

                                    message.Subject = Messages.Subject;
                                    message.Sender = new MailAddress(admin.EmailId, Messages.From); ;
                                    //message.Body = "Scheduled at " + DateTime.Now; //Message body
                                    message.IsBodyHtml = true;
                                    message.Body = Messages.MarriageEmail; //Message body
                                    EmailHelper.Send(message, admin.EmailId, admin.EmailPassword, (Boolean)admin.SSL, admin.Host, admin.Port.Value);
                                }
                            }
                        }
                    }

                    if (item.DateOfIncorporation.HasValue)
                    {
                        if (item.DateOfIncorporation.Value.ToString("dd-MM") == DateTime.Now.ToString("dd-MM"))
                        {
                            var t_dtofinCMSg = dtofinCMSg.Replace("#customername", item.Name);
                            var result = SMSHelper.SendSMS(item.Phone, t_dtofinCMSg,dotempid);

                            if (result.Contains("SMS"))
                            {
                                cntdtofincwishes = cntdtofincwishes + 1;
                            }
                        }

                        if (!item.Email.IsNullOrEmpty())
                        {
                            using (var message = new MailMessage(admin.EmailId, item.Email))
                            {

                                message.Subject = Messages.Subject;
                                message.Sender = new MailAddress(admin.EmailId, Messages.From); ;
                                //message.Body = "Scheduled at " + DateTime.Now; //Message body
                                message.IsBodyHtml = true;
                                message.Body = Messages.DofAnniversaryEmail; //Message body
                                EmailHelper.Send(message, admin.EmailId, admin.EmailPassword, (Boolean)admin.SSL, admin.Host, admin.Port.Value);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }


            //SubContacts wishes
            foreach (var item in (SubContactListOfWishes))
            {
                try
                {
                    if (item.Birthdate.HasValue)
                    {
                        if (item.Birthdate.Value.ToString("dd-MM") == DateTime.Now.ToString("dd-MM"))
                        {
                            var t_bdayMsg = bdayMsg.Replace("#customername", item.Name);
                            var result = SMSHelper.SendSMS(item.Phone, t_bdayMsg,bdtempid);

                            if (result.Contains("SMS"))
                            {
                                cntbdaywishes = cntbdaywishes + 1;
                            }

                            if (!item.Email.IsNullOrEmpty())
                            {
                                using (var message = new MailMessage(admin.EmailId, item.Email))
                                {

                                    message.Subject = Messages.Subject;
                                    message.Sender = new MailAddress(admin.EmailId, Messages.From); ;
                                    //message.Body = "Scheduled at " + DateTime.Now; //Message body
                                    message.IsBodyHtml = true;
                                    message.Body = Messages.DofAnniversaryEmail; //Message body
                                    EmailHelper.Send(message, admin.EmailId, admin.EmailPassword, (Boolean)admin.SSL, admin.Host, admin.Port.Value);
                                }
                            }
                        }
                    }

                    if (item.MarriageAnniversary.HasValue)
                    {
                        if (item.MarriageAnniversary.Value.ToString("dd-MM") == DateTime.Now.ToString("dd-MM"))
                        {
                            var t_aniMsg = aniMsg.Replace("#customername", item.Name);
                            var result = SMSHelper.SendSMS(item.Phone, t_aniMsg,martempid);

                            if (result.Contains("SMS"))
                            {
                                cntmrgwishes = cntmrgwishes + 1;
                            }

                            if (!item.Email.IsNullOrEmpty())
                            {
                                using (var message = new MailMessage(admin.EmailId, item.Email))
                                {

                                    message.Subject = Messages.Subject;
                                    message.Sender = new MailAddress(admin.EmailId, Messages.From); ;
                                    //message.Body = "Scheduled at " + DateTime.Now; //Message body
                                    message.IsBodyHtml = true;
                                    message.Body = Messages.DofAnniversaryEmail; //Message body
                                    EmailHelper.Send(message, admin.EmailId, admin.EmailPassword, (Boolean)admin.SSL, admin.Host, admin.Port.Value);
                                }
                            }
                        }
                    }
                }
                catch(Exception ex) { }
            }

            model.CountOfBirthDayWishes = cntbdaywishes;
            model.CountOfAniversaryWishes = cntmrgwishes;
            model.CountOfIncorporationWishes = cntdtofincwishes;

            var dbSqlStr1 = "INSERT INTO DailyWishesLog(Date,Log) VALUES('" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "','Birthday SMS: " + Convert.ToString(cntbdaywishes) + ", Marriage Anniversary SMS: " + Convert.ToString(cntmrgwishes) + ", Firm Anniversay SMS: " + Convert.ToString(cntdtofincwishes) + "')";

            //Saving to log
            connection.Execute(dbSqlStr1);

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}