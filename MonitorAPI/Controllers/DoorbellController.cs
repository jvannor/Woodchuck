using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UtilityLib.Data;
using MonitorApi.Services;
using System.Collections.Immutable;
using System.Globalization;
using Newtonsoft.Json;

namespace MonitorApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DoorbellController : ControllerBase
    {
        public DoorbellController(ILogger<DoorbellController> logger, IQueueProvider queue)
        {
            _logger = logger;
            _queue = queue;
        }

        [HttpPost]
        public ActionResult<string> Post()
        {
            var alert = new
            {
                From = "Monitor",
                To = "Worker",
                Subject = "Doorbell Notification",
                Time = DateTime.Now
            };

            _queue.PublishMessage(JsonConvert.SerializeObject(alert));
            return Accepted();
        }

        private ILogger<DoorbellController> _logger;
        private IQueueProvider _queue;
    }
}