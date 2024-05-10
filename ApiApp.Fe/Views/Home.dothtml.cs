using ApiApp.Fe.ViewModels;
using DotVVM.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiApp.Fe.Views
{
    public class Home : MasterPageViewModel
    {
        private Random random = new Random();
        public string CurrentThing { get; set; } = "Nic";

        [Bind(Direction.ServerToClientFirstRequest)]
        public string ApiPath { get; set; } = "https://localhost:5011";
        public List<LinearRechartDataDTO> Data { get; set; }
        public List<NavItems> NavList => new List<NavItems> {
            new NavItems() { Href = "/",HrefName = "Domů" },
            new NavItems() { Href = "/Ínfo",HrefName = "Informace" },
        };
        public void Clicked()
        {

        }
        public override Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                Regenerate();
            }

            return base.PreRender();
        }

        public void Regenerate()
        {
            Data = Enumerable.Range(0, 15).Select(s =>
                 new LinearRechartDataDTO()
                 {
                     Line1 = 1000 * random.Next(),
                     Line2 = 1000 * random.Next(),
                     Line3 = 1000 * random.Next(),
                     Name = "Page " + random.Next() + random.Next(),
                 })
                .ToList();
        }

    }
    public class LinearRechartDataDTO
    {
        //{ name: 'Page A', uv: 400, pv: 2400, amt: 0 }, { name: 'Page B', uv: 450, pv: 2800, amt: 2700 }
        public string Name { get; set; }
        public int Line1 { get; set; }
        public int Line2 { get; set; }
        public int Line3 { get; set; }
    }
    public class NavItems
    {
        public string Href { get; set; }
        public string HrefName { get; set; }
    }
}
