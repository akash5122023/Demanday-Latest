using System;
using Quartz;
using Quartz.Impl;

namespace AdvanceCRM.Common.Scheduler
{
    public class Scheduler
    {
        public static async System.Threading.Tasks.Task StartAsync()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            try
            {
                IJobDetail job = JobBuilder.Create<DailyWishesSMSEmail>().Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithDailyTimeIntervalSchedule
                      (s =>
                         
                         s.WithIntervalInHours(24)
                        .OnEveryDay()
                        //.StartingDailyAt(TimeSpan.)
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(09, 00))
                      )
                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception)
            {
                throw new Exception("Error shooting SMS");
            }

            //Tasks
            try
            {
                IJobDetail job = JobBuilder.Create<TaskRecurringScheduler>().Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithDailyTimeIntervalSchedule
                      (s =>

                         s.WithIntervalInHours(24)
                        .OnEveryDay()
                        //.StartingDailyAt(TimeSpan.)
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(09, 00))
                      )
                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception)
            {
                throw new Exception("Error shooting SMS");
            }

            IJobDetail jobEmail = JobBuilder.Create<DailyEmailFollowupsScheduler>().Build();

            ITrigger triggerEmail = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(16, 35))
                  )
                .Build();
            await scheduler.ScheduleJob(jobEmail, triggerEmail);
        }
    }
}