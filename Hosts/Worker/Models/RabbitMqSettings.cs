using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTasksSerice.Worker
{
    internal class RabbitMqSettings
    {
        public const string SectionName = "RabbitMq";
        public string Host { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
    }
}
