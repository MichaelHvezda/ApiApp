using ApiApp.ApiModels;
using ApiApp.Controllers.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Threading.Tasks;

namespace ApiApp.Controllers
{
    [ApiController]
    [Route("Api")]
    public class DataController : ApiBaseController<DataModel, DataController, int>
    {
        public DataController(IUnitOfWorkProvider unitOfWorkProvider, ILogger<DataController> logger) : base(unitOfWorkProvider, logger)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Ahoj()
        {
            LogOk(MethodBase.GetCurrentMethod().Name);
            return Ok("Ahoj");
        }
    }
}
