using ApiApp.ApiModels.Generic;
using ApiApp.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Net.Http;
using System.Reflection;

namespace ApiApp.Controllers.Generic
{
    public class ApiColectionController<TGetModel, TUpdateModel, TInsertModel, TController, TRepository, TEntity, TKey> : ApiController<TGetModel, TUpdateModel, TController, TRepository, TEntity, TKey>
        where TGetModel : BaseModel<TKey>
        where TUpdateModel : BaseModel<TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TRepository : AppRepository<TEntity, TKey>

    {
        protected ApiColectionController(IMapper mapper, IUnitOfWorkProvider unitOfWorkProvider, ILogger<TController> logger, TRepository repositoty, IIncludeDefinition<TEntity>[] includes = default!) : base(mapper, unitOfWorkProvider, logger, repositoty, includes)
        {
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody] TInsertModel model)
        {
            try
            {
                LogInformation(MethodBase.GetCurrentMethod().Name);

                var uow = unitOfWorkProvider.Create();
                var newEntity = mapper.Map<TEntity>(model);
                repositoty.Insert(newEntity);
                await uow.CommitAsync();

                LogOk(MethodBase.GetCurrentMethod().Name);
                return Ok();
            }
            catch (Exception e)
            {
                LogException(MethodBase.GetCurrentMethod().Name, e);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("List")]
        public async Task<IActionResult> AddList([FromBody] List<TInsertModel> modelList)
        {
            try
            {
                LogInformation(MethodBase.GetCurrentMethod().Name);
                var uow = unitOfWorkProvider.Create();
                foreach (var model in modelList)
                {
                    var newEntity = mapper.Map<TEntity>(model);
                    repositoty.Insert(newEntity);
                    await uow.CommitAsync();
                }

                LogOk(MethodBase.GetCurrentMethod().Name);
                return Ok();
            }
            catch (Exception e)
            {
                LogException(MethodBase.GetCurrentMethod().Name, e);
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("{Id}")]
        public virtual async Task<IActionResult> Delete(TKey Id)
        {
            try
            {
                LogInformation(MethodBase.GetCurrentMethod().Name);

                var uow = unitOfWorkProvider.Create();
                repositoty.Delete(Id);
                await uow.CommitAsync();

                LogOk(MethodBase.GetCurrentMethod().Name);
                return Ok();
            }
            catch (Exception e)
            {
                LogException(MethodBase.GetCurrentMethod().Name, e);
                return BadRequest();
            }
        }
    }


    public class ApiController<TGetModel, TUpdateModel, TController, TRepository, TEntity, TKey> : ApiBaseRepositoryController<TGetModel, TController, TRepository, TKey>
    where TGetModel : BaseModel<TKey>
    where TUpdateModel : BaseModel<TKey>
    where TEntity : class, IEntity<TKey>, new()
    where TRepository : AppRepository<TEntity, TKey>
    {
        private readonly IIncludeDefinition<TEntity>[] includes;

        protected ApiController(IMapper mapper, IUnitOfWorkProvider unitOfWorkProvider, ILogger<TController> logger, TRepository repositoty, IIncludeDefinition<TEntity>[] includes = default!) : base(mapper, unitOfWorkProvider, logger, repositoty)
        {
            this.includes = includes;
        }

        [HttpPut]
        [Route("{Id}")]
        public virtual async Task<IActionResult> Update(TKey Id, [FromBody] TUpdateModel model)
        {
            try
            {
                LogInformation(MethodBase.GetCurrentMethod().Name);
                model.Id = Id;
                var uow = unitOfWorkProvider.Create();
                var newTrip = mapper.Map<TEntity>(model);
                repositoty.Update(newTrip);
                await uow.CommitAsync();

                LogOk(MethodBase.GetCurrentMethod().Name);
                return Ok();
            }
            catch (Exception e)
            {
                LogException(MethodBase.GetCurrentMethod().Name, e);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{Id}")]
        public virtual async Task<ActionResult<TGetModel>> GetById(TKey Id)
        {
            try
            {
                LogInformation(MethodBase.GetCurrentMethod().Name);

                var uow = unitOfWorkProvider.Create();
                TEntity entity = default;
                if (includes == null)
                {
                    entity = await repositoty.GetByIdAsync(Id);
                }
                else
                {
                    entity = await repositoty.GetByIdAsync(Id, includes);
                }

                var res = mapper.Map<TGetModel>(entity);

                LogOk(MethodBase.GetCurrentMethod().Name);
                return OkDataResponse(res);
            }
            catch (Exception e)
            {
                LogException(MethodBase.GetCurrentMethod().Name, e);
                return BadRequest();
            }
        }

#if DEBUG 
        /// <summary>
        /// Super slow metod 
        /// </summary>
        /// <returns>Return all items</returns>
        [Obsolete]
        [HttpGet]
        [Route("GetAll")]
        public virtual ActionResult<List<TGetModel>> GetAll()
        {
            try
            {
                LogInformation(MethodBase.GetCurrentMethod().Name);

                var uow = unitOfWorkProvider.Create();
                var data = repositoty.GetAllMapped<TGetModel>();
                var list = data.ToList();

                LogOk(MethodBase.GetCurrentMethod().Name);
                return OkDataResponse(list);
            }
            catch (Exception e)
            {
                LogException(MethodBase.GetCurrentMethod().Name, e);
                return BadRequest();
            }
        }
#endif

    }

    public class ApiBaseRepositoryController<TGetModel, TController, TRepository, TKey> : ApiBaseController<TGetModel, TController, TKey>
    where TGetModel : BaseModel<TKey>
    {

        protected readonly TRepository repositoty;

        protected ApiBaseRepositoryController(IMapper mapper, IUnitOfWorkProvider unitOfWorkProvider, ILogger<TController> logger, TRepository repositoty) : base(mapper, unitOfWorkProvider, logger)
        {
            this.repositoty = repositoty;
        }
    }

    public class ApiBaseController<TGetModel, TController, TKey> : ControllerBase
    where TGetModel : BaseModel<TKey>
    {
        protected readonly IMapper mapper;
        protected readonly IUnitOfWorkProvider unitOfWorkProvider;
        protected readonly ILogger<TController> logger;

        protected ApiBaseController(IMapper mapper, IUnitOfWorkProvider unitOfWorkProvider, ILogger<TController> logger)
        {
            this.mapper = mapper;
            this.unitOfWorkProvider = unitOfWorkProvider;
            this.logger = logger;
        }

        protected ActionResult<TData> OkDataResponse<TData>(TData data)
        {
#if DEBUG
            var res = JsonConvert.SerializeObject(data, Formatting.Indented);
            logger.LogInformation("Ok response with data: " + res);
            return Ok(res);
#else
            return Ok(data);
#endif
        }
        protected ActionResult<IList<TData>> OkDataResponse<TData>(IList<TData> data)
        {
#if DEBUG
            var res = JsonConvert.SerializeObject(data, Formatting.Indented);

            logger.LogInformation("Ok response with data list length: " + data.Count);
            return Ok(res);
#else
            return Ok(data);
#endif
        }

        protected void LogInformation(string MetodName)
        {
            logger.LogInformation(MetodName + " - Call");
        }

        protected void LogOk(string MetodName)
        {
            logger.LogInformation(MetodName + " - Done");
        }

        protected void LogException(string MetodName, Exception e)
        {
            logger.LogError(e, MetodName + " dont work with exception: " + e.ToString());
        }
    }
}
