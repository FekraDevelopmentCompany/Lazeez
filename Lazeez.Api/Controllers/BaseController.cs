using Lazeez.Helper;
using Lazeez.Repository.UnitOfWork;
using System.Web.Http;

namespace Lazeez.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        protected IUnitOfWork unitOfWork = new UnitOfWork();

        public BaseController()
        {
            Localization.SetCulture(GlobalSettings.CurrentCulture);
        }
    }
}