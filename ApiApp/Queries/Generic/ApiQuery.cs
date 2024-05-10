using ApiApp.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Queries.Generic
{
    public abstract class ApiQuery<ResModel> : EntityFrameworkQuery<ResModel, DatabaseContext>
    {
        protected ApiQuery(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }
    }
    public abstract class ApiQuery<ResModel, FilterModel> : ApiQuery<ResModel>
    {
        public FilterModel Filter { get; set; }

        protected ApiQuery(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }
    }
}
