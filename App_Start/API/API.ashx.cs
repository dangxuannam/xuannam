using System.Web;
using JsonServices;
using JsonServices.Web;
namespace CafeOnline.API
{
    /// <summary>
    /// Summary description for API
    /// </summary>
    public class API : JsonHandler
    {
        public API ()
        {
            this.service.Name = "CafeOnline.API";
            this.service.Description = "BossCF API";
            InterfaceConfiguration IConfig = new InterfaceConfiguration("RestAPI", typeof(IService), typeof(Services));
            this.service.Interfaces.Add(IConfig);
        }
    }
}