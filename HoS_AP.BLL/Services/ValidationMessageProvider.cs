using System.Collections.Generic;
using HoS_AP.BLL.ServiceInterfaces;
using HoS_AP.BLL.Validation;

namespace HoS_AP.BLL.Services
{
    using System.Globalization;

    internal class ValidationMessageProvider : IValidationMessageProvider
    {
        public string Get(ValidationMessageKeys key)
        {
            return Resources.Resource.ResourceManager.GetString(key.ToString());
        }
    }
}