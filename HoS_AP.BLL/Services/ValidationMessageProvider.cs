using System.Collections.Generic;
using HoS_AP.BLL.ServiceInterfaces;
using HoS_AP.BLL.Validation;

namespace HoS_AP.BLL.Services
{
    internal class ValidationMessageProvider : IValidationMessageProvider
    {
        //private static readonly IDictionary<ValidationMessageKeys, string> Map = new Dictionary
        //    <ValidationMessageKeys, string>
        //{
        //    {ValidationMessageKeys.Authentication_Invalid_Credentials, Resources.Resource.Authentication_Invalid_Credentials},
        //    {ValidationMessageKeys.Authentication_UserName_Required, Resources.Resource.Authentication_UserName_Required },
        //    {ValidationMessageKeys.Authentication_Password_Required, Resources.Resource.Authentication_Password_Required },
        //    {ValidationMessageKeys.CharacterEdit_Name_Required, Resources.Resource.CharacterEdit_Name_Required},
        //    {ValidationMessageKeys.CharacterEdit_Name_Must_Be_Unique, Resources.Resource.CharacterEdit_Name_Must_Be_Unique },
        //    {ValidationMessageKeys.CharacterEdit_Name_Special_Characters, Resources.Resource.CharacterEdit_Name_Special_Characters },
        //    {ValidationMessageKeys.CharacterEdit_Price_Boundaries, Resources.Resource.CharacterEdit_Price_Boundaries },
        //    {ValidationMessageKeys.CharacterEdit_Type_Required, Resources.Resource.CharacterEdit_Name_Required }
        //};

        public string Get(ValidationMessageKeys key)
        {
            return Resources.Resource.ResourceManager.GetString(key.ToString());
        }
    }
}