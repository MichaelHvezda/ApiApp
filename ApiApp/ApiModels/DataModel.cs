using ApiApp.ApiModels.Generic;

namespace ApiApp.ApiModels
{
    public class DataModel : BaseModel<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }

    }
}
