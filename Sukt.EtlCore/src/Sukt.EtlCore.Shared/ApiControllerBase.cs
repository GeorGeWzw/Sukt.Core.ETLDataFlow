using Microsoft.AspNetCore.Mvc;
using System;

namespace Sukt.EtlCore.Shared
{
    [Route("etl/[controller]/[action]")]
    [ApiController]
    public abstract class ApiControllerBase
    {
    }
}
