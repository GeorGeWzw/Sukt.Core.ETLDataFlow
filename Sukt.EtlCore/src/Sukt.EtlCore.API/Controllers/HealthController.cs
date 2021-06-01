using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Sukt.EtlCore.API.Controllers
{
    [Route("api/healthchecks")]
    //[AllowAnonymous]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;

        public HealthController( ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 健康监测通过liveness来探测微服务的存活性
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("liveness")]
        public IActionResult GetLiveness()
        {
            //await _test.TestIRequset("asdjlasdmlaslda");
            //_logger.LogError("健康探针{liveness}");
            return Ok("ok");
        }
        /// <summary>
        /// 健康监测通过readiness来探测微服务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("readiness")]
        public IActionResult GetReadiness()
        {
            //await _test.TestIRequset("asdjlasdmlaslda");
            //_logger.LogError("健康探针{readiness}");
            return Ok("ok");
        }

    }
}