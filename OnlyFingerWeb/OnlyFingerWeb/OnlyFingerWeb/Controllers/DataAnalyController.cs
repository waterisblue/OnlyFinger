using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlyFingerWeb.Service.DataAnalyService;

namespace OnlyFingerWeb.Controllers
{
    [Route("dataanay/[controller]")]
    [ApiController]
    public class DataAnalyController : ControllerBase
    {
        IDataAnalyService dataAnalyService;
        public DataAnalyController(IDataAnalyService dataAnalyService)
        {
            this.dataAnalyService = dataAnalyService;
        }

        [HttpGet("getCurrentTimeTask")]
        public string getCurrentTimeTask()
        {
            var returnCode = dataAnalyService.getCurrentTimeTask();
            return JsonConvert.SerializeObject(returnCode);
        }
        [HttpPost("getGaugeData")]

        public string getGaugeData(int taskId)
        {
            var returnCode = dataAnalyService.getGaugeData(taskId);
            return JsonConvert.SerializeObject(returnCode);
        }
        [HttpPost("getSignUser")]
        public string getSignUser(int taskId)
        {
            var returnCode = dataAnalyService.getSignUser(taskId);
            return JsonConvert.SerializeObject(returnCode);
        }

        [HttpPost("getTimeAnaly")]
        public string getTimeAnaly(long starttime, long endtime)
        {
            var returnCode = dataAnalyService.getTimeAnaly(starttime, endtime);
            return JsonConvert.SerializeObject(returnCode);
        }
    }
}
