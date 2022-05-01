using ApiApp.Data.Entities;
using AutoMapper;
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
        protected readonly IMapper mapper;

        protected ApiQuery(IMapper mapper, IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
            this.mapper = mapper;
        }
    }
    public abstract class ApiQuery<ResModel, FilterModel> : ApiQuery<ResModel>
    {
        public FilterModel filter { get; set; }

        protected ApiQuery(IMapper mapper, IUnitOfWorkProvider unitOfWorkProvider) : base(mapper,unitOfWorkProvider)
        {
        }
    }
}
