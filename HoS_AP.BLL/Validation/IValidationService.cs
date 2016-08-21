using System.Collections.Generic;
using HoS_AP.BLL.Models;

namespace HoS_AP.BLL.Validation
{
    public interface IValidationService
    {
        ICollection<ValidationError> Validate(AuthenticationModel model);
    }
}