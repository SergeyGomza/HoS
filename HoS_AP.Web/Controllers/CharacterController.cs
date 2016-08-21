using System.Web.Mvc;
using HoS_AP.BLL.Models;
using HoS_AP.BLL.ServiceInterfaces;

namespace HoS_AP.Web.Controllers
{
    [Authorize, RoutePrefix("characters")]
    public class CharacterController : Controller
    {
        private readonly ICharacterPresentationService characterPresentationService;
        private readonly ICharacterOperationService characterOperationService;

        public CharacterController(ICharacterPresentationService characterPresentationService, 
            ICharacterOperationService characterOperationService)
        {
            this.characterPresentationService = characterPresentationService;
            this.characterOperationService = characterOperationService;
        }

        [Route]
        public ActionResult Index()
        {
            return View(characterPresentationService.List());
        }

        [Route("add")]
        public ActionResult Add()
        {
            return View(new CharacterEditModel());
        }

        [Route("add"), HttpPost]
        public ActionResult Add(CharacterEditModel model)
        {
            var operationResult = characterOperationService.Create(model);
            if (operationResult.IsValid)
            {
                return RedirectToAction("Index");
            }

            operationResult.ToModelErrors(ModelState);
            return View(model);
        }

        [Route("{name}/edit")]
        public ActionResult Edit(string name)
        {
            return View();
        }

        [Route("{name}/edit"), HttpPost]
        public ActionResult Edit(string name, CharacterEditModel model)
        {
            return View();
        }
    }
}