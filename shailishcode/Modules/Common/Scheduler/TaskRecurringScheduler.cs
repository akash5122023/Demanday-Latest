using AdvanceCRM.Administration;
using AdvanceCRM.Common;
using AdvanceCRM.Contacts;
using AdvanceCRM.Enquiry;
using AdvanceCRM.Quotation;
using AdvanceCRM.Tasks;
using AdvanceCRM.Services;
using AdvanceCRM.Template;
using Quartz;
using Serenity;
using Serenity.Data;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Mail;
using Serenity.Extensions.DependencyInjection;

namespace AdvanceCRM.Common.Scheduler
{
    public class TaskRecurringScheduler : IJob
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

            //Daily Wishes check
            var ts = TasksRow.Fields;

            var c = ContactsRow.Fields;
            var sc = SubContactsRow.Fields;

            string dt = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00.000";
            string dt1 = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59.000";

            var ContactListOfWishes = connection.List<TasksRow>(cw => cw
            .SelectTableFields()
            .Select(ts.ContactsName)
            .Select(ts.ContactsPhone)
            .Select(ts.CreationDate)
            .Select(ts.Period)
            .Select(ts.ExpectedCompletion)
            .Select(ts.ProductId)
            //.Select(c.DateOfIncorporation)
            .Where(new Criteria("CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate()))
            .Where(ts.Recurring == 1)
            //.Where(ts.Period != DBNull.Value)
            );

            

            var dwtr = Template.DailyWishesTemplateRow.Fields;
            var Messages = connection.TryFirst<DailyWishesTemplateRow>( dw => dw
                    .Select(dwtr.BirthdaySMS)
                    .Select(dwtr.MarriageSMS)
                    .Select(dwtr.DofAnniversarySMS)
                    .Select(dwtr.From)
                    .Select(dwtr.Subject)
                    .Select(dwtr.BirthdayEmail)
                    .Select(dwtr.MarriageEmail)
                    .Select(dwtr.DofAnniversaryEmail)
                   //.Where(dwtr.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                );

            var bdayMsg = Messages.BirthdaySMS;
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
                //try
                //{
                    if (item.ExpectedCompletion.HasValue)
                    {

                        using (var innerConnection = Dependency.Resolve<ISqlConnections>().NewFor<TasksRow>())
                          {
                          DateTime ex= Convert.ToDateTime(item.ExpectedCompletion);
                        dynamic typ, exdate = 0,period=0;
                           
                        if (item.Period==(Masters.TaskPeriodMaster)1) //monthly
                            {
                              period = 1;
                              exdate = ex.AddMonths(1).ToString("yyyy-MM-dd") + " 00:00:00.000";
                            }
                            else if (item.Period == (Masters.TaskPeriodMaster)2)//quaterly
                             {
                            period = 2;
                            exdate = ex.AddMonths(3).ToString("yyyy-MM-dd") + " 00:00:00.000";
                        }
                            else if(item.Period == (Masters.TaskPeriodMaster)3)//half-year
                             {
                            period = 3;
                            exdate = ex.AddMonths(6).ToString("yyyy-MM-dd") + " 00:00:00.000";
                        }
                            else if (item.Period == (Masters.TaskPeriodMaster)4)//year
                             {
                            period = 4;
                            exdate = ex.AddYears(1).ToString("yyyy-MM-dd") + " 00:00:00.000";
                        }

                        string str = "INSERT INTO Tasks([ContactsId],[ProductId],[ProjectId],[Task],[Details],[CreationDate],[ExpectedCompletion],[AssignedBy],[AssignedTo],[StatusId],[TypeId],[Priority],[Recurring],[Period]) VALUES" +
                                   "('" + item.ContactsId + "','" + item.ProductId + "','" + item.ProjectId + "','" + item.Task + "','" + item.Details + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + exdate + "','" + Convert.ToString(item.AssignedBy.Value) + "','" + Convert.ToString(item.AssignedTo.Value) + "','1','1','1','" + item.Recurring + "','" + period + "')";



                        if (item.ProjectId != null)
                        {
                            if (item.ContactsId != null)
                            {
                                if (item.ProductId != null)
                                {
                                    str = "INSERT INTO Tasks([ContactsId],[ProductId],[ProjectId],[Task],[Details],[CreationDate],[ExpectedCompletion],[AssignedBy],[AssignedTo],[StatusId],[TypeId],[Priority],[Recurring],[Period]) VALUES" +
                                    "('" + item.ContactsId + "','" + item.ProductId + "','" + item.ProjectId + "','" + item.Task + "','" + item.Details + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + exdate + "','" + Convert.ToString(item.AssignedBy.Value) + "','" + Convert.ToString(item.AssignedTo.Value) + "','1','1','1','" + item.Recurring + "','" + period + "')";
                                }
                                else
                                {
                                    str = "INSERT INTO Tasks([ContactsId],[ProjectId],[Task],[Details],[CreationDate],[ExpectedCompletion],[AssignedBy],[AssignedTo],[StatusId],[TypeId],[Priority],[Recurring],[Period]) VALUES" +
                                    "('" + item.ContactsId + "','" + item.ProjectId + "','" + item.Task + "','" + item.Details + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + exdate + "','" + Convert.ToString(item.AssignedBy.Value) + "','" + Convert.ToString(item.AssignedTo.Value) + "','1','1','1','" + item.Recurring + "','" + period + "')";
                                }
                            }
                            else
                            {
                                if (item.ProductId != null)
                                {
                                    str = "INSERT INTO Tasks(,[ProductId],[ProjectId],[Task],[Details],[CreationDate],[ExpectedCompletion],[AssignedBy],[AssignedTo],[StatusId],[TypeId],[Priority],[Recurring],[Period]) VALUES" +
                                    "('" + item.ProductId + "','" + item.ProjectId + "','" + item.Task + "','" + item.Details + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + exdate + "','" + Convert.ToString(item.AssignedBy.Value) + "','" + Convert.ToString(item.AssignedTo.Value) + "','1','1','1','" + item.Recurring + "','" + period + "')";

                                }
                                else
                                {
                                    str = "INSERT INTO Tasks([ProjectId],[Task],[Details],[CreationDate],[ExpectedCompletion],[AssignedBy],[AssignedTo],[StatusId],[TypeId],[Priority],[Recurring],[Period]) VALUES" +
                                    "('" + item.ProjectId + "','" + item.Task + "','" + item.Details + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + exdate + "','" + Convert.ToString(item.AssignedBy.Value) + "','" + Convert.ToString(item.AssignedTo.Value) + "','1','1','1','" + item.Recurring + "','" + period + "')";
                                }
                            }
                        }
                        else
                        {
                            if (item.ContactsId != null)
                            {
                                if (item.ProductId != null)
                                {
                                    str = "INSERT INTO Tasks([ContactsId],[ProductId],[Task],[Details],[CreationDate],[ExpectedCompletion],[AssignedBy],[AssignedTo],[StatusId],[TypeId],[Priority],[Recurring],[Period]) VALUES" +
                                    "('" + item.ContactsId + "','" + item.ProductId + "','" + item.Task + "','" + item.Details + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + exdate + "','" + Convert.ToString(item.AssignedBy.Value) + "','" + Convert.ToString(item.AssignedTo.Value) + "','1','1','1','" + item.Recurring + "','" + period + "')";
                                }
                                else
                                {
                                    str = "INSERT INTO Tasks([ContactsId],[Task],[Details],[CreationDate],[ExpectedCompletion],[AssignedBy],[AssignedTo],[StatusId],[TypeId],[Priority],[Recurring],[Period]) VALUES" +
                                    "('" + item.ContactsId + "','" + item.Task + "','" + item.Details + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + exdate + "','" + Convert.ToString(item.AssignedBy.Value) + "','" + Convert.ToString(item.AssignedTo.Value) + "','1','1','1','" + item.Recurring + "','" + period + "')";
                                }
                            }
                            else
                            {
                                if (item.ProductId != null)
                                {
                                    str = "INSERT INTO Tasks([ProductId],[Task],[Details],[CreationDate],[ExpectedCompletion],[AssignedBy],[AssignedTo],[StatusId],[TypeId],[Priority],[Recurring],[Period]) VALUES" +
                                   "('" + item.ProductId + "','" + item.Task + "','" + item.Details + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + exdate + "','" + Convert.ToString(item.AssignedBy.Value) + "','" + Convert.ToString(item.AssignedTo.Value) + "','1','1','1','" + item.Recurring + "','" + period + "')";
                                }
                                else
                                {
                                    str = "INSERT INTO Tasks([Task],[Details],[CreationDate],[ExpectedCompletion],[AssignedBy],[AssignedTo],[StatusId],[TypeId],[Priority],[Recurring],[Period]) VALUES" +
                                    "('" + item.Task + "','" + item.Details + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + exdate + "','" + Convert.ToString(item.AssignedBy.Value) + "','" + Convert.ToString(item.AssignedTo.Value) + "','1','1','1','" + item.Recurring + "','" + period + "')";
                                }
                            }
                        }


                         innerConnection.Execute(str);

                                //    using (var message = new MailMessage(admin.EmailId, "Amitk1116@gmail.com"))
                                //    {
                                //    //message.Subject = Messages.Subject;
                                //    //message.Sender = new MailAddress(admin.EmailId, Messages.From); ;
                                //    //message.Body = "Scheduled at " + DateTime.Now; //Message body
                                //    //message.IsBodyHtml = true;
                                //    //message.Body = Messages.BirthdayEmail; //Message body
                                //    //EmailHelper.Send(message, admin.EmailId, admin.EmailPassword, (Boolean)admin.SSL, admin.Host, admin.Port.Value);
                                //}

                            }
                        }

                   
                }
                //catch (Exception ex)
                //{

                //}
            //}



            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}