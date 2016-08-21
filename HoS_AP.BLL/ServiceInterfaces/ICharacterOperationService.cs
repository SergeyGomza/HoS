using HoS_AP.BLL.Models;

namespace HoS_AP.BLL.ServiceInterfaces
{
    public interface ICharacterOperationService
    {
        ValidationResult Create(CharacterEditModel model);
    }
}