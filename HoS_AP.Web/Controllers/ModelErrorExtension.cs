using System.Web.Mvc;
using HoS_AP.BLL.Models;

namespace HoS_AP.Web.Controllers
{
    public static class ModelErrorExtension
    {
        public static void ToModelErrors(this ValidationResult validationResult, ModelStateDictionary modelState)
        {
            foreach (var validationError in validationResult.Errors)
            {
                modelState.AddModelError(validationError.Property, validationError.Message);
            }
        }
    }
}