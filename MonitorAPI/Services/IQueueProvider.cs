using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorApi.Services
{
    public interface IQueueProvider
    {
        void PublishMessage(string message);
    }
}
