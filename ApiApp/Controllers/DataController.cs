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
        public DataController(IMapper mapper, IUnitOfWorkProvider unitOfWorkProvider, ILogger<DataController> logger) : base(mapper, unitOfWorkProvider, logger)
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
