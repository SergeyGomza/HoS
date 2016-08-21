using System.Web.Mvc;
using HoS_AP.BLL.ServiceInterfaces;

namespace HoS_AP.Web.Controllers
{
    [Authorize, RoutePrefix("characters")]
    public class CharacterController : Controller
    {
        private readonly ICharacterPresentationService characterPresentationService;

        public CharacterController(ICharacterPresentationService characterPresentationService)
        {
            this.characterPresentationService = characterPresentationService;
        }

        [Route]
        public ActionResult Index()
        {
            return View(characterPresentationService.List());
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