using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.ApiModels.Generic
{
    public class BaseModel<TKey>
    {
        public TKey Id { get; set; }
    }
}
