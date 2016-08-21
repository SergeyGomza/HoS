using System.Web.Mvc;

namespace HoS_AP.Web.Controllers
{
    [Authorize, RoutePrefix("characters")]
    public class CharacterController : Controller
    {
        [Route]
        public ActionResult Index()
        {
            return View();
        }

        [Route("add")]
        public ActionResult Add()
        {
            return View();
        }

        [Route("add"), HttpPost]
        public ActionResult Add(object model)
        {
            return View();
        }

        [Route("{name}/edit")]
        public ActionResult Edit(string name)
        {
            return View();
        }

        [Route("{name}/edit"), HttpPost]
        public ActionResult Edit(string name, object model)
        {
            return View();
        }
    }
}