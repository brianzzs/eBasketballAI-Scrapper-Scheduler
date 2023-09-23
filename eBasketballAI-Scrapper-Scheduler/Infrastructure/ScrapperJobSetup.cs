using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    [DisallowConcurrentExecution]
    public class ScrapperJobSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var jobKey = JobKey.Create(nameof(ScrapperJob));
            options
            .AddJob<ScrapperJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
            .AddTrigger(trigger => trigger.ForJob(jobKey)
            .WithSimpleSchedule(schedule => schedule.WithIntervalInMinutes(10).RepeatForever()));
        }
    }
}
