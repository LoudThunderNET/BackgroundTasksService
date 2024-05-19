using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTasksService.AppServices.Services.Models
{
    public class TaskHttpInvokerSettings
    {
        public const string SectionName = "TaskHttpInvokerSettings";
        public TimeSpan Timeout { get; set; }
    }
}
