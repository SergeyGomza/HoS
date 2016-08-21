using System.Collections.Generic;
using HoS_AP.BLL.ServiceInterfaces;
using HoS_AP.BLL.Validation;

namespace HoS_AP.BLL.Services
{
    internal class ValidationMessageProvider : IValidationMessageProvider
    {
        private static readonly IDictionary<ValidationMessageKeys, string> Map = new Dictionary
            <ValidationMessageKeys, string>
        {
            {ValidationMessageKeys.Authentication_Invalid_Credentials, "Sorry, your credentials are not valid"}
        };

        public string Get(ValidationMessageKeys key)
        {
            return Map[key];
        }
    }
}